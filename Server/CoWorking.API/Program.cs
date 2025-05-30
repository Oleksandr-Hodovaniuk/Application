using CoWorking.API.Extensions;
using CoWorking.API.Middlewares;
using CoWorking.Application.Extensions;
using CoWorking.Application.Interfaces.Seeders;
using CoWorking.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add Presentation services to the container.
builder.Services.AddPresentation();

// Register Infrastructure services and configure the database context to the container.
builder.Services.AddInfrastructure(builder.Configuration);

// Register Application services to the container.
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Data seeding.
    using (var scope = app.Services.CreateScope())
    {
        CancellationToken token = app.Lifetime.ApplicationStopping;

        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
        await seeder.SeedAsync(token);
    }
}

// Add middleware for handling exceptions.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
