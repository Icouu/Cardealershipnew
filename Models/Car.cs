namespace CarDealership.Models;

public class Car
{
    public string CarId { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
}
