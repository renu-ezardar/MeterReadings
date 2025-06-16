using AccountMeter.API.Entities;
using AccountMeter.BusinessLogic;
using AccountMeter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

var connectionString = builder.Configuration.GetConnectionString("AccountMeterDBConnectionString");


builder.Services.AddDbContext<AccountMeterTestContext>(
               option => option.UseSqlServer(
                   connectionString,
                   options => options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null))
               );


builder.Services.AddScoped<IReadingsProcessor, ReadingsProcessor>();
builder.Services.AddScoped<IRepository, Repository>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

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
app.MapControllers();



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
