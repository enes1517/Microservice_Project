using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot dosyasýný ekle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Servisleri kaydet
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Gateway üzerinden diðer servisleri görmek için
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.PathToSwaggerGenerator = "/swagger/docs";
    });
}

// Ana sayfaya gidince doðrudan Swagger'a atsýn
app.MapGet("/", () => Results.Redirect("/swagger"));

await app.UseOcelot();

app.Run();