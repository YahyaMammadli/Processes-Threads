

namespace SystemProgramming.Models;

public class Car
{
    public string Plate { get; set; }
    private GasStation station;
    public int Tank { get; set; }

    public Car(string plate, GasStation station, int tank)
    {
        Plate = plate;
        Tank = tank;
        this.station = station;
    }

    public void StartProcess(int liter)
    {
        Console.WriteLine($"Car {Plate} arrived");
        station.Fuel(this, liter);

    }
}
