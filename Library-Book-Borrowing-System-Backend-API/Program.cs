using LibraryApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<LibraryApi.Repositories.IBorrowRecordRepository, LibraryApi.Repositories.BorrowRecordRepository>();
builder.Services.AddScoped<LibraryApi.Services.IBorrowingService, LibraryApi.Services.BorrowingService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();