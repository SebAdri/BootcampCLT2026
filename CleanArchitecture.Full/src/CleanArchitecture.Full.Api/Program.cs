using CleanArchitecture.Full.Api.Endpoints;
using CleanArchitecture.Full.Api.Middleware;
using CleanArchitecture.Full.Application;
using CleanArchitecture.Full.Infrastructure;
using CleanArchitecture.Full.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseValidationExceptionHandling();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.Migrate();
    //DbSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "CleanArchitecture.Full API";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapAccountEndpoints();

app.Run();
