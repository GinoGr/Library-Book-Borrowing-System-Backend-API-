namespace LibraryBookBorrowingSystm.DTOs;

public record BorrowRequestDto(Guid MemberId, Guid BookId);
public record BorrowRecordResponseDto(Guid Id, Guid BookId, Guid MemberId, DateTime BorrowDate, DateTime? ReturnDate, string Status);