using Microsoft.AspNetCore.Mvc;
using LibraryBookBorrowingSystem.Models;
using LibraryBookBorrowingSystem.Services;
using System.ComponentModel.DataAnnotations;

namespace LibraryBookBorrowingSystem.Controllers;

[ApiController]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> Getmembers()
    {
        var members = await _memberService.GetMembers();
        return Ok(members);
    }

    [HttpGet("by-id/{id:guid}")]
    public async Task<ActionResult<Member>> GetmemberById(Guid id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);

        if (member is null)
            return NotFound(new { error = "Member not found." });

        return Ok(member);
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<Member>> GetmemberByEmail(string email)
    {
        var member = await _memberService.GetMemberByEmailAsync(email);
        if (member is null)
        {
            return NotFound(new { error = "Member not found." });
        }

        return Ok(member);
    }

    [HttpGet("by-name/{FullName}")]
    public async Task<ActionResult<Member>> GetmemberByNames(string FullName)
    {

        var member = await _memberService.GetMemberByNameAsync(FullName);
        if (member is null)
        {
            return NotFound (new { error = "Member not found." });
        }

        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<Member>> Createmember([FromBody] Member input)
    {
        if (string.IsNullOrWhiteSpace(input.FullName))
        {
            return BadRequest( new { error = "Full name is required." });
        }
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            return BadRequest( new { error = "Email is required." });
        }
        if (!new EmailAddressAttribute().IsValid(input.Email))
        {
            return BadRequest( new { error = "Invalid email format." });
        }
        if (await _memberService.GetMemberByEmailAsync(input.Email) != null)
        {
            return Conflict(new { error = "Email is already in use." });
        }
        

        var created = await _memberService.CreateMemberAsync(input);
        return CreatedAtAction(nameof(GetmemberById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Member>> Updatemember(Guid id, [FromBody] Member input)
    {
        if (string.IsNullOrWhiteSpace(input.FullName))
        {
            return BadRequest( new { error = "Full name is required." });
        }
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            return BadRequest( new { error = "Email is required." });
        }

        var existing = await _memberService.GetMemberByIdAsync(id);
        if (existing is null)
        {
            return NotFound( new { error = "Member not found." });
        }

        if (input.Email != existing.Email && await _memberService.GetMemberByEmailAsync(input.Email) != null)
        {
            return BadRequest( new { error = "Email is already in use." });
        }

        var updated = await _memberService.UpdateMemberAsync(id, input);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMember(Guid id)
    {
        await _memberService.DeleteMemberAsync(id);
        return NoContent();
    }
}