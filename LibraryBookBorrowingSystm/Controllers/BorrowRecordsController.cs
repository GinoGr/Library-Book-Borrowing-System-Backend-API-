using LibraryBookBorrowingSystm.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBookBorrowingSystm.Controllers;

[ApiController]
[Route("api/borrow-records")]
// TODO: Member 3 — Implement BorrowRecordsController
public class BorrowRecordsController : ControllerBase
{
    private readonly IBorrowService _borrowService;

    public BorrowRecordsController(IBorrowService borrowService)
    {
        _borrowService = borrowService;
    }
}
