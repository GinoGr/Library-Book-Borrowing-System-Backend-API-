using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Exceptions;
using LibraryBookBorrowingSystm.Models;
using LibraryBookBorrowingSystm.Repositories.Interfaces;
using LibraryBookBorrowingSystm.Services.Interfaces;

namespace LibraryBookBorrowingSystm.Services;

public class BorrowService : IBorrowService
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IBookRepository _bookRepository;

    public BorrowService(IBorrowRecordRepository borrowRecordRepository, IBookRepository bookRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BorrowRecordResponse>> GetAllAsync()
    {
        var records = await _borrowRecordRepository.GetAllAsync();
        return records.Select(MapToResponse);
    }

    public async Task<IEnumerable<BorrowRecordResponse>> GetByMemberIdAsync(Guid memberId)
    {
        var records = await _borrowRecordRepository.GetByMemberIdAsync(memberId);
        return records.Select(MapToResponse);
    }

    public async Task<BorrowRecordResponse> BorrowAsync(BorrowBookRequest request)
    {
        if (!await _bookRepository.ExistsAsync(request.BookId))
            throw new NotFoundException("Book not found.");

        var existingRecord = await _borrowRecordRepository.GetActiveRecordAsync(request.BookId, request.MemberId);
        if (existingRecord is not null)
            throw new ValidationException("Member already has an active borrow record for this book.");

        var decremented = await _bookRepository.TryDecrementAvailableCopiesAsync(request.BookId);
        if (!decremented)
            throw new ConflictException("No copies available.");

        var borrowRecord = new BorrowRecord
        {
            Id = Guid.NewGuid(),
            BookId = request.BookId,
            MemberId = request.MemberId,
            BorrowDate = DateTimeOffset.UtcNow,
            Status = "Borrowed"
        };

        await _borrowRecordRepository.AddAsync(borrowRecord);

        borrowRecord.Book = await _bookRepository.GetByIdAsync(request.BookId);
        return MapToResponse(borrowRecord);
    }

    public async Task<BorrowRecordResponse> ReturnAsync(Guid id)
    {
        var record = await _borrowRecordRepository.GetByIdAsync(id);
        if (record is null)
        {
            throw new NotFoundException("Borrow record not found.");
        }

        if (record.Status != "Borrowed")
        {
            throw new ValidationException("No active borrow record found.");
        }

        record.Status = "Returned";
        record.ReturnDate = DateTimeOffset.UtcNow;

        record.Book!.AvailableCopies++;

        await _borrowRecordRepository.UpdateAsync(record);

        return MapToResponse(record);
    }

    private static BorrowRecordResponse MapToResponse(BorrowRecord record)
    {
        return new BorrowRecordResponse
        {
            Id = record.Id,
            BookId = record.BookId,
            MemberId = record.MemberId,
            BookTitle = record.Book?.Title ?? string.Empty,
            MemberName = record.Member?.FullName ?? string.Empty,
            BorrowDate = record.BorrowDate,
            ReturnDate = record.ReturnDate,
            Status = record.Status
        };
    }
}
