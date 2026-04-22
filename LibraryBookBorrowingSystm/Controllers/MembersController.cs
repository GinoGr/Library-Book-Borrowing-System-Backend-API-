using LibraryBookBorrowingSystm.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookBorrowingSystm.Controllers;

[ApiController]
[Route("api/members")]
// TODO: Member 2 — Implement MembersController
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IBorrowService _borrowService;

    public MembersController(IMemberService memberService, IBorrowService borrowService)
    {
        _memberService = memberService;
        _borrowService = borrowService;
    }
}
