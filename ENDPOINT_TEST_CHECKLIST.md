## Books

### Create

- `POST /api/books` with a valid body returns `201 Created`.

```json
{
  "title": "The Pragmatic Programmer",
  "author": "Andrew Hunt",
  "isbn": "978-0201616224",
  "totalCopies": 3
}
```

- Response includes `id`, `title`, `author`, `isbn`, `totalCopies`, and `availableCopies`.
- New book response has `availableCopies` equal to `totalCopies`.
- Save the returned book ID as `BOOK_ID`.
#### PASS

### Validation

- `POST /api/books` without `title` returns `400 Bad Request`.
- `POST /api/books` without `author` returns `400 Bad Request`.
- `POST /api/books` without `isbn` returns `400 Bad Request`.
- `POST /api/books` with `totalCopies: 0` returns `400 Bad Request`.
- `PUT /api/books/{BOOK_ID}` with `availableCopies` greater than `totalCopies` returns `400 Bad Request`.
#### PASS

### Read

- `GET /api/books` returns `200 OK` and a JSON array.
- `GET /api/books/{BOOK_ID}` returns `200 OK`.
- `GET /api/books/00000000-0000-0000-0000-000000000001` returns `404 Not Found`.
#### PASS

### Update

- `PUT /api/books/{BOOK_ID}` with a valid body returns `200 OK`.

```json
{
  "title": "The Pragmatic Programmer (20th Anniversary)",
  "author": "Andrew Hunt",
  "isbn": "978-0201616224",
  "totalCopies": 4,
  "availableCopies": 2
}
```

- Follow-up `GET /api/books/{BOOK_ID}` returns the updated values.
#### PASS

### Delete

- `DELETE /api/books/{BOOK_ID}` returns `204 No Content`.
- Follow-up `GET /api/books/{BOOK_ID}` returns `404 Not Found`.
- `DELETE /api/books/00000000-0000-0000-0000-000000000001` returns `404 Not Found`.
#### PASS

## Members

### Create

- `POST /api/members` with a valid body returns `201 Created`.

```json
{
  "fullName": "Grace Green",
  "email": "grace.green@example.com"
}
```

- Response includes `id`, `fullName`, `email`, and `membershipDate`.
- Save the returned member ID as `MEMBER_ID`.
#### PASS

### Validation

- `POST /api/members` without `fullName` returns `400 Bad Request`.
- `POST /api/members` without `email` returns `400 Bad Request`.
- `POST /api/members` with an invalid email returns `400 Bad Request`.
- `POST /api/members` with a duplicate email returns `409 Conflict`.
- `PUT /api/members/{MEMBER_ID}` with an invalid email returns `400 Bad Request`.
#### PASS

### Read

- `GET /api/members` returns `200 OK` and a JSON array.
- `GET /api/members/{MEMBER_ID}` returns `200 OK`.
- `GET /api/members/00000000-0000-0000-0000-000000000001` returns `404 Not Found`.
#### PASS

### Update

- `PUT /api/members/{MEMBER_ID}` with a valid body returns `200 OK`.

```json
{
  "fullName": "Grace Green Updated",
  "email": "grace.updated@example.com"
}
```

- Follow-up `GET /api/members/{MEMBER_ID}` returns the updated values.
- Updating a member to another existing member's email returns `409 Conflict`.
#### PASS

### Delete

- `DELETE /api/members/{MEMBER_ID}` returns `204 No Content`.
- Follow-up `GET /api/members/{MEMBER_ID}` returns `404 Not Found`.
- `DELETE /api/members/00000000-0000-0000-0000-000000000001` returns `404 Not Found`.
#### PASS

## Borrow Records

### Setup

- Create or reuse a book with `availableCopies` greater than `0`.
- Create or reuse a member.
- Save their IDs as `BORROW_BOOK_ID` and `BORROW_MEMBER_ID`.

### Borrow

- `POST /api/borrow-records/borrow` with a valid body returns `201 Created`.

```json
{
  "bookId": "BORROW_BOOK_ID",
  "memberId": "BORROW_MEMBER_ID"
}
```

- Response includes `id`, `bookId`, `memberId`, `bookTitle`, `memberName`, `borrowDate`, `returnDate`, and `status`.
- New borrow response has `status` equal to `"Borrowed"`.
- New borrow response has `returnDate` equal to `null`.
- Save the returned borrow record ID as `BORROW_RECORD_ID`.
- Follow-up `GET /api/books/{BORROW_BOOK_ID}` shows `availableCopies` decreased by `1`.
#### PASS

### Validation

- `POST /api/borrow-records/borrow` with an unknown `bookId` returns `404 Not Found`.
- `POST /api/borrow-records/borrow` for the same member and book while the first record is still active returns `400 Bad Request`.
#### PASS



- `POST /api/borrow-records/borrow` for a book with no available copies returns `400 Bad Request`.
- `PUT /api/borrow-records/00000000-0000-0000-0000-000000000001/return` returns `404 Not Found`.
#### PASS


### Read

- `GET /api/borrow-records` returns `200 OK` and a JSON array.
- `GET /api/members/{BORROW_MEMBER_ID}/borrow-history` returns `200 OK` and a JSON array.
- Borrow history includes the saved `BORROW_RECORD_ID`.
#### PASS

### Return

- `PUT /api/borrow-records/{BORROW_RECORD_ID}/return` returns `200 OK`.
- Response has `status` equal to `"Returned"`.
- Response has a non-null `returnDate`.
- Follow-up `GET /api/books/{BORROW_BOOK_ID}` shows `availableCopies` increased by `1`.
#### PASS


- Returning the same `BORROW_RECORD_ID` again returns `400 Bad Request`.
#### PASS

