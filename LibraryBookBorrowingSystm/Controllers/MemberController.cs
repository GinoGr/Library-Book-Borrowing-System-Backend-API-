using Microsoft.AspNetCore.Mvc;
using LibraryBookBorrowingSystem.Models;
using LibraryBookBorrowingSystem.Services;
using LibraryBookBorrowingSystm.DTOs.Responses;
using LibraryBookBorrowingSystm.DTOs.Requests;

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
    public async Task<ActionResult<IEnumerable<MemberResponse>>> Getmembers()
    {
        var members = await _memberService.GetMembers();

        var response = members.Select(member => new MemberResponse
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipDate = member.MembershipDate
        });

        return Ok(response);
    }

    [HttpGet("by-id/{id:guid}")]
    public async Task<ActionResult<MemberResponse>> GetMemberById(Guid id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);

        if (member is null)
            return NotFound(new { error = "Member not found." });

        var response = new MemberResponse
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipDate = member.MembershipDate
        };

        return Ok(response);
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<MemberResponse>> GetmemberByEmail(string email)
    {
        var member = await _memberService.GetMemberByEmailAsync(email);
        if (member is null)
        {
            return NotFound(new { error = "Member not found." });
        }

        var response = new MemberResponse
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipDate = member.MembershipDate
        };

        return Ok(response);
    }

    [HttpGet("by-name/{FullName}")]
    public async Task<ActionResult<MemberResponse>> GetmemberByNames(string FullName)
    {

        var member = await _memberService.GetMemberByNameAsync(FullName);
        if (member is null)
        {
            return NotFound (new { error = "Member not found." });
        }

        var response = new MemberResponse
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipDate = member.MembershipDate
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<MemberResponse>> CreateMember([FromBody] CreateMemberRequest input)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = "Invalid input." });

        if (await _memberService.GetMemberByEmailAsync(input.Email) != null)
            return Conflict(new { error = "Email is already in use." });

        var member = new Member
        {
            FullName = input.FullName,
            Email = input.Email
        };

        var created = await _memberService.CreateMemberAsync(member);

        var response = new MemberResponse
        {
            Id = created.Id,
            FullName = created.FullName,
            Email = created.Email,
            MembershipDate = created.MembershipDate
        };

        return CreatedAtAction(nameof(GetMemberById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MemberResponse>> Updatemember(Guid id, [FromBody] UpdateMemberRequest input)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = "Invalid input." });

        var existing = await _memberService.GetMemberByIdAsync(id);
        if (existing is null)
        {
            return NotFound(new { error = "Member not found." });
        }

        if (input.Email != existing.Email &&
            await _memberService.GetMemberByEmailAsync(input.Email) != null)
        {
            return Conflict(new { error = "Email is already in use." });
        }

        var memberToUpdate = new Member
        {
            FullName = input.FullName,
            Email = input.Email
        };

        var updated = await _memberService.UpdateMemberAsync(id, memberToUpdate);

        var response = new MemberResponse
        {
            Id = updated.Id,
            FullName = updated.FullName,
            Email = updated.Email,
            MembershipDate = updated.MembershipDate
        };

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMember(Guid id)
    {
        await _memberService.DeleteMemberAsync(id);
        return NoContent();
    }
}