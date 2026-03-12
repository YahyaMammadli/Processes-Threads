
namespace SystemProgramming.Models;

public class GasStation
{
    public string Name { get; set; }
    private object locker = new();

    public GasStation(string name)
    {
        Name = name;
    }

    public void Fuel(Car car, int liter)
    {
        lock (locker)
        {
            Console.WriteLine($"\n\n\t{car.Plate} started refuelling");

            car.Tank += liter;

            Console.WriteLine($"\t{car.Plate} finished refuelling\n\n");
        }
    }
}

