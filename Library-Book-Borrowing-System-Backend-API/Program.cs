using LibraryBookBorrowingSystm.Middleware;

using LibraryBookBorrowingSystm.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// Register your Database Context (using an in-memory database for local testing)
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseInMemoryDatabase("LibraryDb"));

// Register your Repositories and Services
builder.Services.AddScoped<LibraryBookBorrowingSystm.Repositories.IBorrowRecordRepository, LibraryBookBorrowingSystm.Repositories.BorrowRecordRepository>();
builder.Services.AddScoped<LibraryBookBorrowingSystm.Repositories.IBookRepository, LibraryBookBorrowingSystm.Repositories.BookRepository>();
builder.Services.AddScoped<LibraryBookBorrowingSystm.Services.IBorrowingService, LibraryBookBorrowingSystm.Services.BorrowingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();