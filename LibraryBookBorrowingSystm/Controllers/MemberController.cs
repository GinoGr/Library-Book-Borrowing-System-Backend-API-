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
    public ActionResult<IEnumerable<Member>> Getmembers()
    {
        var members = _memberService.GetMembers();
        return Ok(members);
    }

    [HttpGet("by-id/{id:guid}")]
    public ActionResult<Member> GetmemberById(Guid id)
    {
        var member = _memberService.GetMemberById(id);
        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpGet("by-email/{email}")]
    public ActionResult<Member> GetmemberByEmail(string email)
    {
        var member = _memberService.GetMemberByEmail(email);
        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

   [HttpGet("by-name/{FullName}")]
    public ActionResult<Member> GetmemberByNames(string FullName)
    {

        var member = _memberService.GetMemberByName(FullName);
        if (member is null)
        {
            return NotFound (new { error = "Member not found." });
        }

        return Ok(member);
    }

    [HttpPost]
    public ActionResult<Member> Createmember([FromBody] Member input)
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
        if (_memberService.GetMemberByEmail(input.Email) != null)
        {
            return Conflict(new { error = "Email is already in use." });
        }
        

        var created = _memberService.CreateMember(input);
        return CreatedAtAction(nameof(GetmemberById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<Member> Updatemember(Guid id, [FromBody] Member input)
    {
        if (string.IsNullOrWhiteSpace(input.FullName))
        {
            return BadRequest( new { error = "Full name is required." });
        }
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            return BadRequest( new { error = "Email is required." });
        }

        var existing = _memberService.GetMemberById(id);
        if (existing is null)
        {
            return NotFound( new { error = "Member not found." });
        }

        if (input.Email != existing.Email && _memberService.GetMemberByEmail(input.Email) != null)
        {
            return BadRequest( new { error = "Email is already in use." });
        }

        var updated = _memberService.UpdateMember(id, input);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public ActionResult DeleteMember(Guid id)
    {
        var existing = _memberService.GetMemberById(id);
        if (existing is null)
        {
            return NotFound( new { error = "Member not found." });
        }

        _memberService.DeleteMember(id);
        return NoContent();
    }
}