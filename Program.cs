using CarDealership.Services;

const string filePath = "Data/cars.txt";
CarFileService carFileService = new();
var cars = carFileService.Load(filePath);

Console.WriteLine($"Loaded cars: {cars.Count}");
