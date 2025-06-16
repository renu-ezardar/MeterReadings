using AccountMeter.API.Entities;
using AccountMeter.BusinessLogic;
using AccountMeter.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
