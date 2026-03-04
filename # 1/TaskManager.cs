using System.Diagnostics;


namespace ProcessManager;

enum SortMode
{
    Name,
    Memory
}

class TaskManager
{
    private List<Process> _processes = new List<Process>();
    private string filter = string.Empty;
    private SortMode sortMode = SortMode.Name;





    public void Run()
    {
        Console.Title = "Task Manager";

        
        
        while (true)
        {
            try
            {
                UpdateProcessList();
                Console.Clear();

                DisplayProcesses();

                



                ShowMenu();

                Console.Write(" => ");
                string input = Console.ReadLine().Trim().ToUpper();

                switch (input)
                {
                    case "R":
                        break; 
                    case "K":
                        KillProcess();
                        break;
                    case "Q":
                        Console.Write("Exiting program\n");
                        return;
                    case "S":
                        SetSortMode();
                        break;
                    case "F":
                        SetFilter();
                        break;
                    default:
                        Console.Write("Invalid command. Press any key to continue...\n");
                        Console.ReadKey();
                        break;
                }
            }
            catch
            {


            }

        }


    }






    private void UpdateProcessList()
    {

        try
        {
            var processes = Process.GetProcesses().ToList();


            if (!string.IsNullOrEmpty(filter))
            {
                processes = processes.Where(p => p.ProcessName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            }

            if (sortMode == SortMode.Name)
            {
                processes = processes.OrderBy(p => p.ProcessName).ToList();

            }

            if (sortMode == SortMode.Memory)
            {
                processes = processes.OrderByDescending(p => GetWorkingSet(p)).ToList();
            }


            _processes = processes;





        }
        catch 
        {


        }

    }







    private void DisplayProcesses()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(new string('-', 120) + "\n");
        Console.Write($"{"PID",-8} {"Process Name",-30} {"Memory (MB)",-15} {"Threads",-8} \t{"Start Time",-30}\n");
        Console.Write(new string('-', 120) + "\n");

        long totalThreads = 0;

        string startTime = string.Empty;

        foreach (var process in _processes)
        {
            try
            {
                startTime = process.StartTime.ToString("dd.MM.yyyy HH:mm:ss");

                Console.Write($"\n\n{process.Id,-8}{process.ProcessName,-30}{GetWorkingSet(process)/ (1024*1024),-15:N0} {process.Threads.Count,-10} \t{startTime,-30}\n\n");

                totalThreads+= process.Threads.Count;

            }
            catch 
            {



            }

        }




        

        Console.Write(new string('-', 120) + "\n");
        Console.Write($"\n\nTotal processes: {_processes.Count}, total threads: {totalThreads}\n\n\n\n");
        Console.Write(new string('-', 120) + "\n\n\n\n");

    }









    private long GetWorkingSet(Process proc)
    {
        try
        {
            return proc.WorkingSet64;
        }
        catch
        {
            
            return 0;
        
        }

    }






    private void ShowMenu()
    {
        Console.Write("\n\nMenu:\n");
        Console.Write("  R - refresh list\n");
        Console.Write("  K - kill process by ID\n");
        Console.Write("  S - change sorting\n");
        Console.Write("  F - set filter by name\n");
        Console.Write("  Q - quit\n");
    }









    private void KillProcess()
    {
        Console.Write("Enter process ID to terminate => ");
        string input = Console.ReadLine().Trim();

        if (!int.TryParse(input, out int pid))
        {
            Console.Write("Invalid ID. Press any key...\n");
            Console.ReadKey();
            return;
        }


        try
        {

            Process proc = Process.GetProcessById(pid);


            if (!proc.HasExited)
            {

                Console.Write("Attempting to close main window...\n");

                if (proc.CloseMainWindow())
                {
                    Console.Write("Main window closed, waiting for process to exit...\n");


                    if (proc.WaitForExit(3000))
                    {
                        Console.Write("Process exited gracefully\n");
                        return;
                    }

                    else
                    {
                        Console.Write("Process did not exit, applying Kill...\n");

                    }




                }

                else
                {
                    Console.Write("Could not close main window. Applying Kill()...\n");
                }




                proc.Kill();
                proc.WaitForExit(2000);
                Console.Write("Process forcefully terminated\n\n");







            }

            else
            {
                Console.Write("Process already exited.\n");
            }




        }
        catch 
        {


        }












        Console.Write("Press any key...\n");
        Console.ReadKey();
    }





    private void SetSortMode()
    {
        Console.Write("Sort by (N)ame or (M)emory => ");
        string input = Console.ReadLine().Trim().ToUpper();

        switch (input)
        {
            case "N":
                sortMode = SortMode.Name;
                break;

            case "M":
                sortMode = SortMode.Memory;
                break;

            default:
                Console.WriteLine("Invalid choice.");
                Console.ReadKey();
                return;
        }


    }







    private void SetFilter()
    {
        
        Console.Write("Enter filter string for process name (Enter to reset) => ");
        
        string input = Console.ReadLine().Trim();
        
        filter = input;
        
        Console.Write($"Filter set to: '{filter}'. Press any key...\n");
        
        Console.ReadKey();


    }



}


