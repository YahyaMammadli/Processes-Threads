using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Channels;

namespace Program;

class Program
{


    // 1. Thread
    static void DemonstrateThread()
    {
        Console.WriteLine("Demonstrating Thread:");
        Thread thread = new Thread(() => 
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread: {i}");
            }
        });
        thread.Start();  // Start
        thread.Join();   // Wait for completion
        Console.WriteLine("Main thread completed");
    }



    // 2. Thread Pool
    static void DemonstrateThreadPool()
    {
        Console.WriteLine("\nDemonstrating ThreadPool:");
        ThreadPool.QueueUserWorkItem(state => 
        {
            for (int i = 0; i < 3; i++)
            {

                Console.WriteLine($"Pool: {i}");
            }
        });
        Thread.Sleep(500);  // To give time for execution (in real code, use Task or events)
    }



    // 3. BackgroundWorker
    static void DemonstrateBackgroundWorker()
    {
        Console.WriteLine("\nDemonstrating BackgroundWorker:");
        BackgroundWorker worker = new BackgroundWorker();
        worker.DoWork += (s, e) => { Console.WriteLine("Working"); };
        worker.RunWorkerCompleted += (s, e) => Console.WriteLine("Completed");
        worker.RunWorkerAsync();
        Thread.Sleep(500);  // Wait
    }



    // 4. Parallel Extensions (Parallel.For)
    static void DemonstrateParallelFor()
    {
        Console.WriteLine("\nDemonstrating Parallel.For:");
        Parallel.For(0, 5, i => 
        {
            Console.WriteLine($"Parallel: {i}");
        });
    }



    // 5. Task
    static async Task DemonstrateTask()
    {
        Console.WriteLine("\nDemonstrating Task:");
        Task task = Task.Run(() => Console.WriteLine("Task"));
        await task;
    }



    // 6. async/await
    static async Task DemonstrateAsyncAwait()
    {
        Console.WriteLine("\nDemonstrating async/await:");
        await Task.Delay(1000);
        Console.WriteLine("After delay");
    }



    // 7. ThreadLocal<T>
    static void DemonstrateThreadLocal()
    {
        Console.WriteLine("\nDemonstrating ThreadLocal:");
        ThreadLocal<int> local = new ThreadLocal<int>(() => 42);
        Console.WriteLine(local.Value);  // 42
        Thread thread = new Thread(() => Console.WriteLine(local.Value));  // 42 in another thread
        thread.Start();
        thread.Join();
    }



    // 8. Channels
    static async Task DemonstrateChannels()
    {
        Console.WriteLine("\nDemonstrating Channels:");
        var channel = Channel.CreateUnbounded<int>();
        await channel.Writer.WriteAsync(1);
        var value = await channel.Reader.ReadAsync();
        Console.WriteLine(value);  // 1
    }



    // 9. ValueTask
    static async Task DemonstrateValueTask()
    {
        Console.WriteLine("\nDemonstrating ValueTask:");
        ValueTask<int> GetValue() => new ValueTask<int>(42);
        int result = await GetValue();
        Console.WriteLine(result);
    }



    // 10. Async streams (IAsyncEnumerable)
    static async Task DemonstrateAsyncStreams()
    {
        Console.WriteLine("\nDemonstrating Async Streams:");
        async IAsyncEnumerable<int> Stream()
        {
            for (int i = 0; i < 3; i++)
            { 
                await Task.Delay(100); yield return i; 
            }
        }
        await foreach (var item in Stream()) Console.WriteLine(item);
    }



    // 11. Monitor
    static void DemonstrateMonitor()
    {
        Console.WriteLine("\nDemonstrating Monitor:");
        object obj = new object();
        Monitor.Enter(obj);
        try { Console.WriteLine("Critical section"); } finally { Monitor.Exit(obj); }
    }



    // 12. lock
    static void DemonstrateLock()
    {
        Console.WriteLine("\nDemonstrating lock:");
        object locker = new object();
        lock (locker) { Console.WriteLine("Locked"); }
    }



    // 13. Mutex
    static void DemonstrateMutex()
    {
        Console.WriteLine("\nDemonstrating Mutex:");
        using (Mutex mutex = new Mutex(true, "GlobalMutex"))
        {
            if (mutex.WaitOne(0)) { Console.WriteLine("Access granted"); } else { Console.WriteLine("Busy"); }
        }
    }



    // 14. Semaphore / SemaphoreSlim
    static void DemonstrateSemaphore()
    {
        Console.WriteLine("\nDemonstrating SemaphoreSlim:");
        SemaphoreSlim sem = new SemaphoreSlim(2);  // Max 2
        sem.Wait(); Console.WriteLine("Enter"); sem.Release();
    }



    // 15. Interlocked
    static void DemonstrateInterlocked()
    {
        Console.WriteLine("\nDemonstrating Interlocked:");
        int counter = 0;
        Interlocked.Increment(ref counter);
        Console.WriteLine(counter);  // 1
    }



    // 16. ManualResetEvent
    static void DemonstrateManualResetEvent()
    {
        Console.WriteLine("\nDemonstrating ManualResetEvent:");
        ManualResetEvent evt = new ManualResetEvent(false);
        Thread thread = new Thread(() => { Thread.Sleep(500); evt.Set(); });
        thread.Start();
        evt.WaitOne();
        Console.WriteLine("Signal received");
    }



    static volatile bool flag = false;

    // 17. Volatile
    static void DemonstrateVolatile()
    {
        Console.WriteLine("\nDemonstrating Volatile:");
        flag = false;
        Thread thread = new Thread(() => 
        { 
            flag = true; 
        });
        thread.Start();
        thread.Join();
        Console.WriteLine(flag);  // true
    }



    // 18. ReaderWriterLockSlim
    static void DemonstrateReaderWriterLockSlim()
    {
        Console.WriteLine("\nDemonstrating ReaderWriterLockSlim:");
        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        rwLock.EnterReadLock(); Console.WriteLine("Reading"); rwLock.ExitReadLock();
    }



    // 19. SpinLock
    static void DemonstrateSpinLock()
    {
        Console.WriteLine("\nDemonstrating SpinLock:");
        SpinLock spin = new SpinLock();
        bool taken = false;
        spin.Enter(ref taken); Console.WriteLine("Locked"); spin.Exit();
    }



    // 20. Barrier
    static void DemonstrateBarrier()
    {
        Console.WriteLine("\nDemonstrating Barrier:");
        Barrier barrier = new Barrier(2);
        Thread thread = new Thread(() => barrier.SignalAndWait());
        thread.Start();
        barrier.SignalAndWait();
        thread.Join();
        Console.WriteLine("Barrier passed");
    }



    // 21. Concurrent Collections (ConcurrentDictionary)
    static void DemonstrateConcurrentDictionary()
    {
        Console.WriteLine("\nDemonstrating ConcurrentDictionary:");
        ConcurrentDictionary<int, string> dict = new ConcurrentDictionary<int, string>();
        dict.TryAdd(1, "Value");
        Console.WriteLine(dict[1]);
    }



    // 22. AsyncLocal<T>
    static async Task DemonstrateAsyncLocal()
    {
        Console.WriteLine("\nDemonstrating AsyncLocal:");
        AsyncLocal<int> local = new AsyncLocal<int>();
        local.Value = 42;
        await Task.Delay(1);
        Console.WriteLine(local.Value);  // 42
    }









    static void Main()
    {




        #region Basic mechanisms for working with streams



        #region Thread
        //2002: .NET Framework 1.0 — Thread
        //Базовый класс для ручного создания и управления потоками.
        //Простой, но требует полного контроля: запуск, присоединение, прерывание.
        //Простой, но требует полного контроля: запуск, присоединение, прерывание.
        //Подходит для простых фоновых задач, но не рекомендуется для новых проектов из-за сложности.
        //В ранних версиях .NET это был основной способ, но привёл к проблемам масштабируемости в серверных приложениях.

        DemonstrateThread();

        #endregion


        #region Thread Pool
        //2002: .NET Framework 1.0 — Thread Pool
        //Пул готовых потоков для коротких задач, чтобы избежать overhead создания новых потоков.
        //Автоматически управляет потоками (количество зависит от CPU).
        //Не для долгих операций, нет лёгкой отмены.
        //Улучшил производительность для веб-серверов (ASP.NET), где много кратковременных запросов. В современных .NET интегрирован с Task.

        DemonstrateThreadPool();


        #endregion


        #region Background Worker
        //2005: .NET Framework 2.0 — BackgroundWorker
        //Упрощение для фоновых задач в UI-приложениях (WinForms/WPF).
        //Поддерживает события для прогресса, завершения, отмены.
        //Ограничен UI-контекстом, не для всех сценариев.
        //Добавил отмену через CancellationPending, что решило проблему отсутствия отмены в Thread.
        //Устарел в пользу Task, но всё ещё используется в legacy-коде.

        DemonstrateBackgroundWorker();





        #endregion


        #region Parallel Extensions
        //2010: .NET Framework 4.0 — Parallel Extensions (Parallel.For, Parallel.ForEach)
        //Для параллельного выполнения циклов и задач с данными.
        //Использует Thread Pool под капотом.
        //Фокус на параллелизме данных (например, обработка массивов на многоядерных CPU).
        //Поддерживает параллелизм до степени, указанной в ParallelOptions.
        //Улучшил производительность в вычислительных задачах.


        DemonstrateParallelFor();




        #endregion


        #region Task Parallel Library
        //2010: .NET Framework 4.0 — Task Parallel Library (TPL, Task)
        //Task — абстракция для асинхронных операций.
        //Может использовать пул, поддерживает цепочки (ContinueWith), отмену (CancellationToken), обработку ошибок.
        //Не обязательно создаёт новый поток.
        //Решил проблемы Thread: легче масштабируется, интегрируется с async.
        //Ключевой сдвиг к declarative стилю (что делать, а не как).

        DemonstrateParallelFor();





        #endregion


        #region async/await
        //2012: C# 5.0 (.NET Framework 4.5) — async/await
        //Ключевые слова для асинхронного кода, делают его читаемым как синхронный.
        //Работает с Task.
        //Упрощает работу с I/O (файлы, сеть), избегает блокировки потоков.
        //В эволюции — шаг к асинхронному программированию, особенно для веб/мобильных приложений.

        DemonstrateAsyncAwait();





        #endregion


        #region ThreadLocal<T>
        //2010: .NET Framework 4.0 — ThreadLocal<T> (добавлено как дополнительная конструкция)
        //Хранит данные, уникальные для каждого потока (thread-local storage).
        //Полезно для избежания race conditions без lock.
        //Идеально для сценариев, где каждый поток нуждается в своём экземпляре (например, буферы).

        DemonstrateThreadLocal();





        #endregion


        #region Value Task
        //2019: C# 8.0 (.NET Core 3.0) — ValueTask & async streams (IAsyncEnumerable)
        //ValueTask — оптимизация Task для снижения аллокаций (struct вместо class).
        //Async streams — для асинхронной итерации (yield return в async).
        //Фокус на производительности в облаке/микросервисах.
        //ValueTask для hot paths, async streams для стриминга данных (например, из БД).

        DemonstrateValueTask();
        DemonstrateAsyncStreams();

        #endregion



        #endregion




        #region Synchronization Mechanisms



        #region Monitor
        //2002: .NET Framework 1.0 — Monitor
        //Базовый класс для синхронизации: Enter/Exit для mutual exclusion, Wait/Pulse для сигналов.
        //Низкоуровневый, риск deadlock'ов, но фундамент для всех lock.

        DemonstrateMonitor();




        #endregion


        #region lock
        //2002: C# 1.0 — lock (statement)
        //Syntactic sugar над Monitor: автоматический Enter/Exit с finally.
        //Предпочтительный для простой блокировки.
        //Простота, безопасность от исключений.
        //Минус: Может блокироваться надолго.


        DemonstrateLock();



        #endregion


        #region Mutex
        //2002: .NET Framework 1.0 — Mutex
        //Похож на Monitor, но кросс-процессный, может быть named.
        //Для глобальной синхронизации (например, один экземпляр приложения).
        //Медленнее из-за kernel.

        DemonstrateMutex();


        #endregion


        #region Semaphore
        //2002: .NET Framework 1.0 — Semaphore
        //Ограничивает количество потоков (max N).
        //Для пулов ресурсов.
        //SemaphoreSlim (2007, .NET 3.5) — лёгкая версия без kernel.


        DemonstrateSemaphore();





        #endregion


        #region Interlocked
        //2002: .NET Framework 1.0 — Interlocked
        //Атомарные операции без lock.
        //Быстро для простых типов, избегает overhead.


        DemonstrateInterlocked();




        #endregion


        #region ManualResetEvent / AutoResetEvent
        //2002: .NET Framework 1.0 — ManualResetEvent / AutoResetEvent
        //События для сигналов.
        //Manual — ручной сброс, Auto — автоматический.
        //Для координации (WaitOne/Set).


        DemonstrateManualResetEvent();






        #endregion


        #region Volatile
        //2002: .NET Framework 1.0 — Volatile
        //Ключевое слово для volatile полей: гарантирует видимость изменений между потоками без lock.
        //Для простых флагов, но не полная атомарность.



        DemonstrateVolatile();





        #endregion


        #region Reader Writer Lock
        //2005: .NET Framework 2.0 — ReaderWriterLock
        //Множество читателей, один писатель.
        //Оптимизация для read-heavy.
        //ReaderWriterLockSlim (2007, .NET 3.5) — улучшенная версия.
        //Минус: Риск starvation.


        DemonstrateReaderWriterLockSlim();




        #endregion


        #region Spin Lock
        //2010: .NET Framework 4.0 — SpinLock
        //Busy-wait блокировка без kernel.
        //Для микросекундных операций на многоядерных системах. Тратит CPU.


        DemonstrateSpinLock();




        #endregion


        #region Barrier
        //2010: .NET Framework 4.0 — Barrier
        //Синхронизирует потоки на барьере.
        //Для фаз в параллельных вычислениях.



        DemonstrateBarrier();




        #endregion


        #region Concurrent Collections
        //2010: .NET Framework 4.0 — Concurrent Collections (например, ConcurrentDictionary)
        //Thread-safe коллекции.
        //Встроенная синхронизация, идеально для shared данных.



        DemonstrateConcurrentDictionary();



        #endregion


        #region AsyncLocal<T>
        //2015: .NET Framework 4.6 — AsyncLocal<T>
        //Аналог ThreadLocal, но для async контекста (ExecutionContext).
        //Сохраняет данные через await (например, логгинг контекста).



        DemonstrateAsyncLocal();




        #endregion



        #endregion




    }


}
