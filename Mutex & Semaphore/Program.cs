

namespace Program;

public class Program
{





    static void Main()
    {


        #region Messenger

        bool isUnique;


        Mutex mutex = new Mutex(true, $"John Messenger", out isUnique);

        if (!isUnique)
        {
            Console.Write("Document is already opened in another editor\n\n");
            Console.Write("Press any key to close this window\n\n");
            Console.ReadKey();
            return;
        }





        var readFile = Task.Run(() =>
        {
            Console.Write("Reading data in file...\n\n");

            Thread.Sleep(3000);

            Console.WriteLine("Succsess analizing file...\n\n ");

        });


        var loadDocument = readFile.ContinueWith(readFile =>
        {

            Console.Write("Loading file...\n\n");

            Thread.Sleep(3000);

            Console.WriteLine("Succsess loading file...\n\n ");



        });


        var openEditor = loadDocument.ContinueWith(loadDocument =>
        {
            Console.Write("Editing document...\n\n");

            Thread.Sleep(3000);

            Console.Write("Document was edited successfully...\n\n\n");

            Console.ReadKey();





        });


        openEditor.Wait();

        #endregion










        var tasks = new Task[5];


        



        Semaphore printerSemaphore = new Semaphore(1, 1, "John Messenger");




        for (int requestId = 1; requestId <= 5; requestId++)
        {
            int id = requestId;

            Console.Write($"Employee {id} sends document to printer...\n\n");

            tasks[requestId - 1] = Task.Run(() =>
            {
                printerSemaphore.WaitOne();

                try
                {
                    Console.Write($"\tEmployee {id} started printing.......\n\n");
                    Thread.Sleep(3000);
                    Console.Write($"\t\tEmployee {id} finished printing X\n\n\n");
                }
                finally
                {
                    printerSemaphore.Release();
                }
            });

            Thread.Sleep(1000);
        }

        Task.WaitAll(tasks);













    }
}
