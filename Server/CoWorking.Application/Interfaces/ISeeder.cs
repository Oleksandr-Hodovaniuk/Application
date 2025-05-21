namespace CoWorking.Application.Interfaces;

public interface ISeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}
