using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введите двоичное число:");
        string binaryNumber = Console.ReadLine();

        Console.Write("Введите количество потоков:");
        int numThreads = int.Parse(Console.ReadLine());

        Thread.CurrentThread.Name = "main";
        ConvertBinaryToDecimal(binaryNumber);

        Thread[] threads = new Thread[numThreads];
        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(ConvertBinaryToDecimal);
            threads[i].Name = $"Thread {i + 1}";
            threads[i].Start(binaryNumber);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.ReadLine(); 
    }

    static void ConvertBinaryToDecimal(object binaryNumberObj) // своя копия
    {
        string binaryNumber = (string)binaryNumberObj;
        Console.WriteLine();
        Console.WriteLine(Thread.CurrentThread.Name + " начал работу...");

        Stopwatch stpWatch = new Stopwatch();
        stpWatch.Start();

        int decimalNumber = 0;
        for (int i = binaryNumber.Length - 1, j = 0; i >= 0; i--, j++)
        {
            int bit = int.Parse(binaryNumber[i].ToString());
            decimalNumber += bit * (int)Math.Pow(2, j);
        }

        Console.WriteLine($"Десятичное число: {decimalNumber}");

        stpWatch.Stop();
        Console.WriteLine("StopWatch: " + stpWatch.ElapsedTicks.ToString());
        Console.WriteLine(Thread.CurrentThread.Name + " закончил работу...");
    }
}
