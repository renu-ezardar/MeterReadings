using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

if (!environmentName.Equals("Local") && !environmentName.Equals("Development"))
{
    ConfigureKestrelHttps(builder);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/health");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

void ConfigureKestrelHttps(WebApplicationBuilder builder)
{
    var certFile = Environment.GetEnvironmentVariable("TLS_CERT_FILE");
    var keyFile = Environment.GetEnvironmentVariable("TLS_PRIV_KEY_FILE");
    _ = int.TryParse(Environment.GetEnvironmentVariable("SERVICE_PORT"), out var httpsPort);

    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
        serverOptions.ListenAnyIP(httpsPort, listenOptions =>
        {
            var cert = X509Certificate2.CreateFromPemFile(certFile, keyFile);
            listenOptions.UseHttps(cert);
        }));
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
