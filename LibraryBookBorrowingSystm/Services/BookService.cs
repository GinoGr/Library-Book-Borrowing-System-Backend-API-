using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Exceptions;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;
using LibraryBookBorrowingSystm.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryBookBorrowingSystm.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMemoryCache _cache;

    // Separate list and per-book entries so writes can invalidate only what changed.
    private const string ListCacheKey = "books:list";
    private static string IdCacheKey(Guid id) => $"books:{id}";

    // Sliding expiration keeps hot entries warm without letting them live indefinitely.
    private static readonly MemoryCacheEntryOptions CacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
        .SetSlidingExpiration(TimeSpan.FromSeconds(30));

    public BookService(IBookRepository bookRepository, IMemoryCache cache)
    {
        _bookRepository = bookRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<BookResponse>> GetAllAsync()
    {
        if (_cache.TryGetValue(ListCacheKey, out IEnumerable<BookResponse>? cached) && cached != null)
            return cached;

        var books = await _bookRepository.GetAllAsync();
        var response = books.Select(MapToResponse).ToList();
        _cache.Set(ListCacheKey, response, CacheOptions);
        return response;
    }

    public async Task<BookResponse> GetByIdAsync(Guid id)
    {
        var key = IdCacheKey(id);
        if (_cache.TryGetValue(key, out BookResponse? cached) && cached != null)
            return cached;

        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) throw new NotFoundException($"Book with ID {id} was not found.");

        var response = MapToResponse(book);
        _cache.Set(key, response, CacheOptions);
        return response;
    }

    public async Task<BookResponse> AddAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.TotalCopies  // New books start fully available.
        };
        await _bookRepository.AddAsync(book);

        // Force the next list read to include the newly added book.
        _cache.Remove(ListCacheKey);

        return MapToResponse(book);
    }

    public async Task<BookResponse> UpdateAsync(Guid id, UpdateBookRequest request)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) throw new NotFoundException($"Book with ID {id} was not found.");

        // This invariant spans two fields, so it is enforced here instead of with property attributes.
        if (request.AvailableCopies > request.TotalCopies)
            throw new Exceptions.ValidationException("AvailableCopies cannot exceed TotalCopies.");

        book.Title = request.Title;
        book.Author = request.Author;
        book.ISBN = request.ISBN;
        book.TotalCopies = request.TotalCopies;
        book.AvailableCopies = request.AvailableCopies;

        await _bookRepository.UpdateAsync(book);

        // Refresh both views because this update affects item and collection responses.
        _cache.Remove(IdCacheKey(id));
        _cache.Remove(ListCacheKey);

        return MapToResponse(book);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (!await _bookRepository.ExistsAsync(id))
            throw new NotFoundException($"Book with ID {id} was not found.");

        await _bookRepository.DeleteAsync(id);

        _cache.Remove(IdCacheKey(id));
        _cache.Remove(ListCacheKey);
    }

    private static BookResponse MapToResponse(Book book) => new BookResponse
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        ISBN = book.ISBN,
        TotalCopies = book.TotalCopies,
        AvailableCopies = book.AvailableCopies
    };
}
