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
        case "2":
            AddCar(cars, carFileService, filePath);
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
    Console.WriteLine("2. Add new car");
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

static void AddCar(List<Car> cars, CarFileService carFileService, string filePath)
{
    string? carId = ReadRequiredText("Car ID: ");

    if (carId is null)
    {
        return;
    }

    if (cars.Any(car => car.CarId.Equals(carId, StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("A car with this ID already exists.");
        return;
    }

    string? make = ReadRequiredText("Make: ");

    if (make is null)
    {
        return;
    }

    string? model = ReadRequiredText("Model: ");

    if (model is null)
    {
        return;
    }

    Console.Write("Year: ");
    bool validYear = int.TryParse(Console.ReadLine(), out int year) && year > 0;

    Console.Write("Price: ");
    bool validPrice = decimal.TryParse(Console.ReadLine(), out decimal price) && price >= 0;

    if (!validYear || !validPrice)
    {
        Console.WriteLine("The car was not added. Check the entered values.");
        return;
    }

    cars.Add(new Car
    {
        CarId = carId,
        Make = make,
        Model = model,
        Year = year,
        Price = price,
        Available = true
    });

    carFileService.Save(filePath, cars);
    Console.WriteLine("Car added successfully.");
}

static string? ReadRequiredText(string prompt)
{
    Console.Write(prompt);
    string? value = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(value))
    {
        Console.WriteLine("This field is required.");
        return null;
    }

    return value;
}
