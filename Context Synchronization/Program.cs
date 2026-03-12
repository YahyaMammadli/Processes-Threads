using SystemProgramming.Models;

namespace Program;

public class Program
{
    static void Main()
    {

        var station = new GasStation("Step It fuel station");

        var car_1 = new Car("10-AA-101", station, 0);
        var car_2 = new Car("10-BB-202", station, 0);
        var car_3 = new Car("10-CC-303", station, 0);
        var car_4 = new Car("10-DD-404", station, 0);
        var car_5 = new Car("10-EE-505", station, 0);

        var t1 = Task.Run(() => car_1.StartProcess(1900000000));
        var t2 = Task.Run(() => car_2.StartProcess(1900000000));
        var t3 = Task.Run(() => car_3.StartProcess(1900000000));
        var t4 = Task.Run(() => car_4.StartProcess(1900000000));
        var t5 = Task.Run(() => car_5.StartProcess(1900000000));


        Task.WaitAll(t1, t2, t3, t4, t5);

        Console.Write($"\n\n\n\n\nCar {car_1.Plate}  Tank {car_1.Tank}\n\n");
        Console.Write($"\nCar {car_2.Plate}  Tank {car_2.Tank} \n");
        Console.Write($"\nCar {car_3.Plate}  Tank {car_3.Tank} \n");
        Console.Write($"\nCar {car_4.Plate}  Tank {car_4.Tank} \n");
        Console.Write($"\nCar {car_5.Plate}  Tank {car_5.Tank} \n");


    }
}
