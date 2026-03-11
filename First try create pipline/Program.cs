using Program.Models;

namespace Program;

public class Program
{
    static void Main()
    {



        #region Library analizyng

        //List<Book> books = new List<Book>
        //{
        //    new Book { Title = "C# Basics", Pages = 320, Price = 30 },
        //    new Book { Title = "Algorithms", Pages = 540, Price = 45 },
        //    new Book { Title = "Clean Code", Pages = 420, Price = 50 },
        //    new Book { Title = "Microservices", Pages = 610, Price = 55 }
        //};

        //var countingPages = Task<int>.Run(() =>
        //{
        //    int page = 0;

        //    foreach (var book in books)
        //    {
        //        page += book.Pages;
        //    }

        //    return page;


        //});



        //Console.Write($"Total pages => {countingPages.Result}\n");





        //var averagePrice = Task<int>.Run(() =>
        //{



        //    return books.Average(x => x.Price); 


        //});

        //Console.Write($"Average price => {averagePrice.Result}\n");


        //var longestBook = Task<string>.Run(() =>
        //{
        //    return books.MaxBy(x => x.Pages)?.Title;

        //});



        //Console.Write($"Longest book => {longestBook.Result}\n");


        #endregion








        #region Order processing pipeline

        var orders = new List<Order>
        {
            new Order { Id = 1, Customer = "Alice", Amount = 120 },
            new Order { Id = 2, Customer = "Bob", Amount = 80 },
            new Order { Id = 3, Customer = "Charlie", Amount = 250 },
            new Order { Id = 4, Customer = "David", Amount = 40 },
            new Order { Id = 5, Customer = "Eva", Amount = 310 }
        };

        var loadingOrdersFromDatabase = Task<List<Order>>.Run(() =>
        {

            Console.Write("Loading orders from database...\n\n");
            Thread.Sleep(2_000);

            return orders;


        });



        var filteringExpensiveOrders = loadingOrdersFromDatabase.ContinueWith(data =>
        {

            Console.Write("Filtering expensive orders...\n\n");
            Thread.Sleep(2_000);

            

            return data.Result.Where(x => x.Amount > 100);



        });

        var customerWithMaxAmount = filteringExpensiveOrders.ContinueWith(filterData =>
        {


            Console.Write(filterData.Result.MaxBy(x => x.Amount) + "\n\n");

        });


        var calculatingTotalAmount = filteringExpensiveOrders.ContinueWith(filterData =>
        {
            Console.Write("Calculating total amount...\n\n");

            Thread.Sleep(2_000);


            Console.Write($"Total amount of expensive orders => {filterData.Result.Sum(x => x.Amount)}\n\n");



        });


        calculatingTotalAmount.Wait();


        #endregion










    }


}
