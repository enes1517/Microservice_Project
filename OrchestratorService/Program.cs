using OrchestratorService.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Refit Client Kaydý
builder.Services.AddRefitClient<IProductServiceApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7081")); // ProductService'in gerçek URL'si

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // /swagger/v1/swagger.json adresini oluþturur
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();