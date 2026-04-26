using LibraryBookBorrowingSystm.DTOs.Requests;
using LibraryBookBorrowingSystm.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookBorrowingSystm.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _memberService.GetAllAsync();
        return Ok(members);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var member = await _memberService.GetByIdAsync(id);
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });

        var member = await _memberService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });

        var member = await _memberService.UpdateAsync(id, request);
        return Ok(member);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _memberService.DeleteAsync(id);
        return NoContent();
    }
}
