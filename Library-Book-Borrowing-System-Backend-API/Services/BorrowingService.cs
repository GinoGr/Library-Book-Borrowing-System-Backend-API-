using LibraryApi.DTOs;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services;

public interface IBorrowingService
{
    Task<BorrowRecordResponseDto> BorrowBookAsync(BorrowRequestDto request);
    Task<BorrowRecordResponseDto> ReturnBookAsync(Guid memberId, Guid bookId);
    Task<IEnumerable<BorrowRecordResponseDto>> GetMemberHistoryAsync(Guid memberId);
}

public class BorrowingService : IBorrowingService
{
    private readonly IBorrowRecordRepository _borrowRepo;
    private readonly IBookRepository _bookRepo;

    public BorrowingService(IBorrowRecordRepository borrowRepo, IBookRepository bookRepo)
    {
        _borrowRepo = borrowRepo;
        _bookRepo = bookRepo;
    }

    public async Task<BorrowRecordResponseDto> BorrowBookAsync(BorrowRequestDto request)
    {
        var existingRecord = await _borrowRepo.GetActiveBorrowRecordAsync(request.MemberId, request.BookId);
        if (existingRecord is not null)
            throw new ArgumentException("Member already has an active borrow record for this book.");

        var book = await _bookRepo.GetByIdAsync(request.BookId);
        if (book is null) throw new KeyNotFoundException("Book not found.");
        
        if (book.AvailableCopies <= 0)
            throw new ArgumentException("No copies available to borrow.");

        book.AvailableCopies--; 

        var borrowRecord = new BorrowRecord
        {
            Id = Guid.NewGuid(),
            BookId = request.BookId,
            MemberId = request.MemberId,
            BorrowDate = DateTime.UtcNow,
            Status = "Borrowed"
        };

        try
        {
            // We update the book and add the record. EF Core will run the [ConcurrencyCheck] here.
            await _bookRepo.UpdateAsync(book); 
            await _borrowRepo.AddAsync(borrowRecord);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new InvalidOperationException("The book was just borrowed by another user. Please try again.");
        }

        return new BorrowRecordResponseDto(borrowRecord.Id, borrowRecord.BookId, borrowRecord.MemberId, borrowRecord.BorrowDate, borrowRecord.ReturnDate, borrowRecord.Status);
    }

    public async Task<BorrowRecordResponseDto> ReturnBookAsync(Guid memberId, Guid bookId)
    {
        var record = await _borrowRepo.GetActiveBorrowRecordAsync(memberId, bookId);
        if (record is null)
            throw new ArgumentException("No active borrow record found for this member and book.");

        record.Status = "Returned";
        record.ReturnDate = DateTime.UtcNow;

        record.Book!.AvailableCopies++;

        await _borrowRepo.UpdateAsync(record);

        return new BorrowRecordResponseDto(record.Id, record.BookId, record.MemberId, record.BorrowDate, record.ReturnDate, record.Status);
    }

    public async Task<IEnumerable<BorrowRecordResponseDto>> GetMemberHistoryAsync(Guid memberId)
    {
        var history = await _borrowRepo.GetHistoryForMemberAsync(memberId);
        return history.Select(b => new BorrowRecordResponseDto(b.Id, b.BookId, b.MemberId, b.BorrowDate, b.ReturnDate, b.Status));
    }
}