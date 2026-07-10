using CarDealership.Models;
using CarDealership.Services;

const string filePath = "Data/cars.txt";
CarFileService carFileService = new();
List<Car> cars = carFileService.Load(filePath);
bool isRunning = true;

while (isRunning)
{
    ShowMenu();
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowAllCars(cars);
            break;
        case "0":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Invalid menu choice.");
            break;
    }

    Console.WriteLine();
}

static void ShowMenu()
{
    Console.WriteLine("Car Dealership Management");
    Console.WriteLine("1. Show all cars");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
}

static void ShowAllCars(List<Car> cars)
{
    if (cars.Count == 0)
    {
        Console.WriteLine("No cars found.");
        return;
    }

    foreach (Car car in cars)
    {
        string availability = car.Available ? "Available" : "Sold";
        Console.WriteLine($"{car.CarId} | {car.Make} {car.Model} | {car.Year} | {car.Price:F2} | {availability}");
    }
}
