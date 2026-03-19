using ConvertingApp.Converter.Models;




namespace ConvertingApp.ConsoleApp;

public class Program
{

    static void Converter(double amount, string from, string to)
    {
        var result = Currency.Convert(amount, from, to);

        Console.Write($"\n\nResult => {result} {to}\n\n");



    }


    static void Main()
    {
        Console.Write("Enter amount => ");
        var amount = int.Parse(Console.ReadLine()!);

        Console.Write("\n\nEnter current currency (AZN, USD, EUR) => ");
        var from = Console.ReadLine()!;

        Console.Write("\n\nEnter currency you want to convert to (AZN, USD, EUR) => ");
        var to = Console.ReadLine()!;



        Converter(amount, from, to);





    }


}
