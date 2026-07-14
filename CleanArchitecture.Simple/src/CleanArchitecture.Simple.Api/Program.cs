using CleanArchitecture.Simple.Api.Endpoints;
using CleanArchitecture.Simple.Application;
using CleanArchitecture.Simple.Infrastructure;
using CleanArchitecture.Simple.Infrastructure.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "CleanArchitecture.Simple API";
    });

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(db);
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapCustomerEndpoints();

app.Run();
