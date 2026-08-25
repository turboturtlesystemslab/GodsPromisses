using GodsPromises.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// Services
// ============================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============================================
// CORS
// ============================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGodsPromisesWeb", policy =>
    {
        policy
            .WithOrigins("https://localhost:7113")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ============================================
// holybible_api
// ============================================

builder.Services.AddHttpClient<BibleService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:35907/");
});

// ============================================
// Build application
// ============================================

var app = builder.Build();

// ============================================
// Swagger
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ============================================
// HTTP Pipeline
// ============================================

app.UseHttpsRedirection();

app.UseCors("AllowGodsPromisesWeb");

app.UseAuthorization();

app.MapControllers();

// ============================================
// Start
// ============================================

app.Run();