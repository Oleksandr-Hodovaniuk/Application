using CoWorking.Application.Extensions;
using CoWorking.Application.Interfaces.Seeders;
using CoWorking.Infrastructure.Extensions;
using CoWorking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Infrastructure services and configure the database context.
builder.Services.AddInfrastructure(builder.Configuration);

// Register Application services.
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        CancellationToken token = app.Lifetime.ApplicationStopping;

        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
        await seeder.SeedAsync(token);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
