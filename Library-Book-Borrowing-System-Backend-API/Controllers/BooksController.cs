using Microsoft.AspNetCore.Mvc;
using LibraryApi.DTOs;
using LibraryApi.Services;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/borrowing")]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;

    public BorrowingController(IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }

    [HttpPost("borrow")]
    public async Task<ActionResult<BorrowRecordResponseDto>> BorrowBook([FromBody] BorrowRequestDto request)
    {
        var record = await _borrowingService.BorrowBookAsync(request);
        return CreatedAtAction(nameof(GetMemberHistory), new { memberId = record.MemberId }, record);
    }

    [HttpPost("return")]
    public async Task<ActionResult<BorrowRecordResponseDto>> ReturnBook([FromBody] BorrowRequestDto request)
    {
        var record = await _borrowingService.ReturnBookAsync(request.MemberId, request.BookId);
        return Ok(record);
    }

    [HttpGet("history/{memberId:guid}")]
    public async Task<ActionResult<IEnumerable<BorrowRecordResponseDto>>> GetMemberHistory(Guid memberId)
    {
        var history = await _borrowingService.GetMemberHistoryAsync(memberId);
        return Ok(history);
    }
}