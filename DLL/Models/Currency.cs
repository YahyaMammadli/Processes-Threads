

namespace ConvertingApp.Converter.Models;

public class Currency
{
    static double AZN = 1.7;
    static double USD = 1;
    static double EUR = 0.87;


    public static double Convert(double amount, string from, string to)
    {
        var fromRate = GetRate(from);

        var toRate = GetRate(to);

        var amountInUsd = amount / fromRate;

        return amountInUsd* toRate;




    }



    private static double GetRate(string currency)
    {

        switch (currency.ToUpper())
        {

            case "AZN": 
                return AZN;
            case "USD":
                return USD;
            case "EUR":
                return EUR;
            default: throw new Exception($"Unknown currency => {currency}\n");
                
        }

    }
     



}
