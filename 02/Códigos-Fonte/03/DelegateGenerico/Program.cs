using System;

class Program
{
    static void Main()
    {
        Temperature t = new Temperature();

        Func<double, double> convertToCelsius = t.ToCelsius;
        Func<double, double> convertToFahrenheit = t.ToFahrenheit;

        double celsius = convertToCelsius(90);
        double fahrenheit = convertToFahrenheit(25);

        Console.WriteLine(celsius);
        Console.WriteLine(fahrenheit);

        Action<double> printCelsius = t.PrintCelsius;
        Action<double> printFahrenheit = t.PrintFahrenheit;

        printCelsius(80);
        printFahrenheit(20);
    }
}

class Temperature
{
    public double ToFahrenheit(double celsius)
    {
        return (celsius * 9 / 5) + 32;
    }

    public double ToCelsius(double fahrenheit)
    {
        return (fahrenheit - 32) * 5 / 9;
    }

    public void PrintFahrenheit(double celsius)
    {
        Console.WriteLine(ToFahrenheit(celsius));
    }

    public void PrintCelsius(double fahrenheit)
    {
        Console.WriteLine(ToCelsius(fahrenheit));
    }
}
