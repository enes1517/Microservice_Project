using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddOcelot();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Use CORS
app.UseCors("AllowAngular");

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
