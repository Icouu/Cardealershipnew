using System.Globalization;
using CarDealership.Models;

namespace CarDealership.Services;

public class CarFileService
{
    private const char Separator = '|';

    public List<Car> Load(string filePath)
    {
        List<Car> cars = new();

        if (!File.Exists(filePath))
        {
            return cars;
        }

        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split(Separator);

            if (parts.Length != 6 ||
                !int.TryParse(parts[3], out int year) ||
                !decimal.TryParse(parts[4], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price) ||
                !bool.TryParse(parts[5], out bool available))
            {
                continue;
            }

            cars.Add(new Car
            {
                CarId = parts[0],
                Make = parts[1],
                Model = parts[2],
                Year = year,
                Price = price,
                Available = available
            });
        }

        return cars;
    }

    public void Save(string filePath, List<Car> cars)
    {
        string? directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        List<string> lines = cars.Select(car => string.Join(Separator,
            car.CarId,
            car.Make,
            car.Model,
            car.Year,
            car.Price.ToString(CultureInfo.InvariantCulture),
            car.Available)).ToList();

        File.WriteAllLines(filePath, lines);
    }
}
