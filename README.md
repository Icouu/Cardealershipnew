# Управление на автокъща

Конзолно C# приложение за добавяне, продажба, търсене и преглед на автомобили.

## Стартиране

Необходим е .NET 8 SDK. От папката на проекта изпълнете:

```bash
dotnet run
```

## Данни за автомобилите

Автомобилите се пазят във файла `Data/cars.txt`. Всеки автомобил е на отделен ред в следния формат:

```text
CarId|Make|Model|Year|Price|Available
```

Пример:

```text
CAR001|Toyota|Corolla|2020|18000.00|true
```

`Available` е `true` за наличен автомобил и `false` за продаден автомобил.

## Menu опции

1. Show all cars
2. Add new car
3. Sell car
4. Check availability by make and/or model
0. Exit

При добавяне или продажба файлът с автомобили се обновява автоматично.
