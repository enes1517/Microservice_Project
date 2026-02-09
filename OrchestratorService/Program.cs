using OrchestratorService.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// CORS Policy ekle - Angular için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

// ProductService için Refit client
builder.Services.AddRefitClient<IProductServiceApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7265"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS'u kullan
app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
