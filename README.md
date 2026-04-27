# Library Book Borrowing System

ASP.NET Core Web API implementing a library book borrowing system with REST architecture, multi-layer design, async programming, SQLite persistence, global exception handling, and in-memory caching.

## Project Structure

```
LibraryBookBorrowingSystm/
├── Controllers/
│   ├── BooksController.cs
│   ├── MembersController.cs
│   └── BorrowRecordsController.cs
├── DTOs/
│   ├── Requests/
│   │   ├── CreateBookRequest.cs
│   │   ├── UpdateBookRequest.cs
│   │   ├── CreateMemberRequest.cs
│   │   ├── UpdateMemberRequest.cs
│   │   └── BorrowBookRequest.cs
│   └── Responses/
│       ├── BookResponse.cs
│       ├── MemberResponse.cs
│       └── BorrowRecordResponse.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── ApplicationDbContextFactory.cs
├── Exceptions/
│   ├── NotFoundException.cs
│   ├── ConflictException.cs
│   └── ValidationException.cs
├── Middleware/
│   └── GlobalExceptionMiddleware.cs
├── Models/
│   ├── Book.cs
│   ├── Member.cs
│   └── BorrowRecord.cs
├── Repositories/
│   ├── Interfaces/
│   │   ├── IBookRepository.cs
│   │   ├── IMemberRepository.cs
│   │   └── IBorrowRecordRepository.cs
│   ├── BookRepository.cs
│   ├── MemberRepository.cs
│   └── BorrowRecordRepository.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IBookService.cs
│   │   ├── IMemberService.cs
│   │   └── IBorrowService.cs
│   ├── BookService.cs
│   ├── MemberService.cs
│   └── BorrowService.cs  
├── appsettings.json
└── Program.cs
```

## API Endpoints

### Books
| Method | Route | Description | Status |
|--------|-------|-------------|--------|
| GET | `/api/books` | Get all books (cached) | 200 |
| GET | `/api/books/{id}` | Get book by ID (cached) | 200 |
| POST | `/api/books` | Create a book | 201 |
| PUT | `/api/books/{id}` | Update a book | 200 |
| DELETE | `/api/books/{id}` | Delete a book | 204 |

### Members
| Method | Route | Description | Status |
|--------|-------|-------------|--------|
| GET | `/api/members` | Get all members | 200 |
| GET | `/api/members/{id}` | Get member by ID | 200 |
| POST | `/api/members` | Create a member | 201 |
| PUT | `/api/members/{id}` | Update a member | 200 |
| DELETE | `/api/members/{id}` | Delete a member | 204 |

### Borrow Records
| Method | Route | Description | Status |
|--------|-------|-------------|--------|
| GET | `/api/borrow-records` | Get all borrow records | 200 |
| POST | `/api/borrow-records/borrow` | Borrow a book | 201 |
| PUT | `/api/borrow-records/{id}/return` | Return a book | 200 |
| GET | `/api/members/{id}/borrow-history` | Borrow history for a member | 200 |


## Business Rules

### Books

- `Title`, `Author`, and `ISBN` are required.
- `TotalCopies` must be greater than `0`.
- `AvailableCopies` must be `0` or greater.
- `AvailableCopies` cannot be greater than `TotalCopies`.
- Newly created books start with `AvailableCopies` equal to `TotalCopies`.

### Members

- `FullName` is required.
- `Email` is required and must be a valid email format.
- Member email addresses must be unique.

### Borrowing

- The book must exist before it can be borrowed.
- A member cannot borrow the same book twice while the first record is still active.
- A book can only be borrowed when at least one copy is available.
- Borrowing decreases `AvailableCopies` by `1`.
- Returning a book marks the borrow record as `Returned`, sets `ReturnDate`, and increases `AvailableCopies` by `1`.
- A borrow record can only be returned while its status is `Borrowed`.

## Error Responses

All handled errors return the same JSON shape:

```json
{
  "error": "Descriptive message"
}
```

Status code mapping:

| Status | When it is used |
|---|---|
| `400 Bad Request` | Invalid request body or business validation failure |
| `404 Not Found` | Requested book, member, or borrow record does not exist |
| `409 Conflict` | Duplicate member email or no copies available during an atomic borrow attempt |
| `500 Internal Server Error` | Unexpected server error |

Unexpected server errors are logged internally and returned to clients as a generic message.

## Architecture

The application uses a layered architecture:

| Layer | Responsibility |
|---|---|
| Controllers | Routes, HTTP status codes, and request delegation |
| Services | Business rules, validation, and DTO mapping |
| Repositories | EF Core queries and database commands |
| Data | `ApplicationDbContext` and migrations |
| Middleware | Consistent exception-to-response handling |

Dependencies flow through interfaces registered in `Program.cs`, which keeps controllers and services decoupled from concrete implementations.

## Caching

Book read endpoints are cached with `IMemoryCache`:

- `GET /api/books` uses key `books:list`
- `GET /api/books/{id}` uses key `books:{id}`
- Absolute expiration: 2 minutes
- Sliding expiration: 30 seconds

Book create, update, and delete operations invalidate affected cache entries so later reads return fresh data.

## Concurrency

Borrowing uses an atomic SQL update to prevent two simultaneous requests from borrowing the last available copy:

```sql
UPDATE Books
SET AvailableCopies = AvailableCopies - 1
WHERE Id = @bookId AND AvailableCopies > 0
```

If the update affects one row, the borrow succeeds. If it affects zero rows, the API returns `409 Conflict` with `"No copies available."`

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- `dotnet-ef` global tool

### Database Setup (first time)

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.11
dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate
dotnet ef database update
```
### Run the API

```bash
cd LibraryBookBorrowingSystm
dotnet watch run
```

Swagger UI will be available at `https://localhost:{port}/swagger`.