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
        case "3":
            SellCar(cars, carFileService, filePath);
            break;
        case "4":
            CheckAvailability(cars);
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
    Console.WriteLine("3. Sell car");
    Console.WriteLine("4. Check availability");
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

static void SellCar(List<Car> cars, CarFileService carFileService, string filePath)
{
    string? carId = ReadRequiredText("Car ID: ");

    if (carId is null)
    {
        return;
    }

    string? model = ReadRequiredText("Model: ");

    if (model is null)
    {
        return;
    }

    Car? carToSell = cars.FirstOrDefault(car =>
        car.CarId.Equals(carId, StringComparison.OrdinalIgnoreCase) &&
        car.Model.Equals(model, StringComparison.OrdinalIgnoreCase));

    if (carToSell is null)
    {
        Console.WriteLine("No car with this ID and model was found.");
        return;
    }

    if (!carToSell.Available)
    {
        Console.WriteLine("This car is already sold.");
        return;
    }

    carToSell.Available = false;
    carFileService.Save(filePath, cars);
    Console.WriteLine("Car sold successfully.");
}

static void CheckAvailability(List<Car> cars)
{
    Console.Write("Make (leave blank to skip): ");
    string make = (Console.ReadLine() ?? string.Empty).Trim();

    Console.Write("Model (leave blank to skip): ");
    string model = (Console.ReadLine() ?? string.Empty).Trim();

    if (string.IsNullOrWhiteSpace(make) && string.IsNullOrWhiteSpace(model))
    {
        Console.WriteLine("Enter a make, a model, or both.");
        return;
    }

    List<Car> matchingCars = cars.Where(car =>
        (string.IsNullOrWhiteSpace(make) || car.Make.Equals(make, StringComparison.OrdinalIgnoreCase)) &&
        (string.IsNullOrWhiteSpace(model) || car.Model.Equals(model, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    if (matchingCars.Count == 0)
    {
        Console.WriteLine("No matching cars found.");
        return;
    }

    foreach (Car car in matchingCars)
    {
        string availability = car.Available ? "Available" : "Sold";
        Console.WriteLine($"{car.CarId} | {car.Make} {car.Model} | {car.Price:F2} | {availability}");
    }
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

    if (value.Contains('|'))
    {
        Console.WriteLine("The character | is not allowed.");
        return null;
    }

    return value;
}
