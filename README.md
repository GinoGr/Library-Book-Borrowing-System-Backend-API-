# Library Book Borrowing System

ASP.NET Core Web API implementing a library book borrowing system with REST architecture, multi-layer design, async programming, SQLite persistence, global exception handling, and in-memory caching.

---

## Project Structure

```
LibraryBookBorrowingSystm/
├── Controllers/
│   ├── BooksController.cs
│   ├── MembersController.cs
│   └── BorrowRecordsController.cs (placeholder)
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
│   └── BorrowRecordRepository.cs  (placeholder)
├── Services/
│   ├── Interfaces/
│   │   ├── IBookService.cs
│   │   ├── IMemberService.cs
│   │   └── IBorrowService.cs
│   ├── BookService.cs
│   ├── MemberService.cs
│   └── BorrowService.cs           (placeholder)
├── appsettings.json
└── Program.cs
```

---

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

### Borrow Records (TODO)
| Method | Route | Description | Status |
|--------|-------|-------------|--------|
| GET | `/api/borrow-records` | Get all borrow records | 200 |
| POST | `/api/borrow-records/borrow` | Borrow a book | 201 |
| PUT | `/api/borrow-records/{id}/return` | Return a book | 200 |
| GET | `/api/members/{id}/borrow-history` | Borrow history for a member | 200 |

---

## Implemented Features

### Books CRUD
Full create, read, update, delete for books with async EF Core data access.

### Members CRUD
Full create, read, update, delete for members with async EF Core data access and unique email validation.

### Caching
`GET /api/books` and `GET /api/books/{id}` are cached with `IMemoryCache`:
- TTL: 2 minutes absolute, 30 seconds sliding
- Cache is invalidated on any POST, PUT, or DELETE to `/api/books`

### Error Handling
All unhandled exceptions are caught by `GlobalExceptionMiddleware` and returned as:
```json
{ "error": "Descriptive message here" }
```

### Validation
| Layer | What |
|-------|------|
| Controller | `[Required]`, `[Range]` via DataAnnotations + ModelState |
| Service | Business rules (copy count constraints) |
| Database | NOT NULL, UNIQUE constraints |

---

## TODO

### Borrow Records
- [ ] Implement `BorrowService` (borrow/return logic, eligibility checks)
- [ ] Implement `BorrowRecordRepository` (including atomic borrow decrement)
- [ ] Implement `BorrowRecordsController` (all 4 endpoints)
- [ ] Concurrency handling: atomic `UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE Id = @bookId AND AvailableCopies > 0`; return 409 if `rowsAffected == 0`

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- `dotnet-ef` global tool

### Run the API

```bash
cd LibraryBookBorrowingSystm
dotnet ef database update
dotnet run
```

Swagger UI will be available at `https://localhost:{port}/swagger`.

### Database Setup (first time)

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.11
dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Business Rules

### Book Rules
- Title, Author, ISBN are required
- `TotalCopies` must be > 0
- `AvailableCopies` must be between 0 and `TotalCopies`

### Member Rules
- FullName is required
- Email is required and must be a valid format
- Email must be unique

### Borrow Rules (TODO)
- A book can only be borrowed if `AvailableCopies > 0`
- A borrow record cannot be returned if its status is already `"Returned"`
