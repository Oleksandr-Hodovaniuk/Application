namespace CoWorking.Application.Interfaces.Seeders;

public interface ISeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}
