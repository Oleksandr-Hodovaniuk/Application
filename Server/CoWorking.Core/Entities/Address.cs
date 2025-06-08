namespace CoWorking.Core.Entities;

public class Address
{
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public int BuildingNumber { get; set; }
}

