using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookBorrowingSystm.Controllers;

[ApiController]
[Route("api/borrow-records")]
public class BorrowRecordsController : ControllerBase
{
    private readonly IBorrowService _borrowService;

    public BorrowRecordsController(IBorrowService borrowService)
    {
        _borrowService = borrowService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BorrowRecordResponse>>> GetAll()
    {
        var records = await _borrowService.GetAllAsync();
        return Ok(records);
    }

    [HttpPost("borrow")]
    public async Task<ActionResult<BorrowRecordResponse>> BorrowBook([FromBody] BorrowBookRequest request)
    {
        var record = await _borrowService.BorrowAsync(request);
        return CreatedAtAction(nameof(GetMemberHistory), new { memberId = record.MemberId }, record);
    }

    [HttpPut("{id:guid}/return")]
    public async Task<ActionResult<BorrowRecordResponse>> ReturnBook(Guid id)
    {
        var record = await _borrowService.ReturnAsync(id);
        return Ok(record);
    }

    [HttpGet("/api/members/{memberId:guid}/borrow-history")]
    public async Task<ActionResult<IEnumerable<BorrowRecordResponse>>> GetMemberHistory(Guid memberId)
    {
        var history = await _borrowService.GetByMemberIdAsync(memberId);
        return Ok(history);
    }
}
