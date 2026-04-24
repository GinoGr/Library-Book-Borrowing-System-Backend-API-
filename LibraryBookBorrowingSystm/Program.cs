using LibraryBookBorrowingSystem.Data;
using Microsoft.EntityFrameworkCore;
using LibraryBookBorrowingSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "library.db");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<LibraryBookBorrowingSystem.Repositories.IMemberRepository, LibraryBookBorrowingSystem.Repositories.MemberRepository>();
builder.Services.AddScoped<LibraryBookBorrowingSystem.Services.IMemberService, LibraryBookBorrowingSystem.Services.MemberService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();