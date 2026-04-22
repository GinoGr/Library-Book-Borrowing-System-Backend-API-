using Microsoft.AspNetCore.Mvc;
using LibraryBookBorrowingSystem.Models;
using LibraryBookBorrowingSystem.Services;

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

   [HttpGet("by-name/{firstName}/{lastName}")]
    public ActionResult<Member> GetmemberByNames(string firstName, string lastName)
    {
        var member = _memberService.GetMemberByName(firstName, lastName);
        if (member is null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPost]
    public ActionResult<Member> Createmember([FromBody] Member input)
    {
        if (string.IsNullOrWhiteSpace(input.FirstName) || string.IsNullOrWhiteSpace(input.LastName))
        {
            return BadRequest("First and last name are required.");
        }
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            return BadRequest("Email is required.");
        }
        if (_memberService.GetMemberByEmail(input.Email) != null)
        {
            return BadRequest("Email is already in use.");
        }
        

        var created = _memberService.CreateMember(input);
        return CreatedAtAction(nameof(GetmemberById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<Member> Updatemember(Guid id, [FromBody] Member input)
    {
        if (string.IsNullOrWhiteSpace(input.FirstName) || string.IsNullOrWhiteSpace(input.LastName))
        {
            return BadRequest("First and last name are required.");
        }
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            return BadRequest("Email is required.");
        }

        var existing = _memberService.GetMemberById(id);
        if (existing is null)
        {
            return NotFound();
        }

        if (input.Email != existing.Email && _memberService.GetMemberByEmail(input.Email) != null)
        {
            return BadRequest("Email is already in use.");
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
            return NotFound();
        }

        _memberService.DeleteMember(id);
        return NoContent();
    }
}