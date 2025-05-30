using System;
using System.Threading;

namespace threaddemo
{
    class Program
    {
        
        private volatile bool canStop = false;

        // К-сть потоків
        private int numberOfThreads = 4;
        private int[] steps = { 1, 2, 3, 4 };

        static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void Start()
        {
            // Запускаємо керуючий потік
            new Thread(Stoper).Start();

            // Запускаємо робочі потоки з різними кроками
            for (int i = 0; i < numberOfThreads; i++)
            {
                int threadId = i + 1;
                int step = steps[i];
                new Thread(() => Calculator(threadId, step)).Start();
            }
        }

        void Calculator(int id, int step)
        {
            long sum = 0;
            int count = 0;
            int currentNumber = 0;

            while (!canStop)
            {
                sum += currentNumber;
                count++;
                currentNumber += step;

                Thread.Sleep(10);  // щоб не зациклювати CPU
            }

            Console.WriteLine($"Потік #{id}: сума = {sum}, кількість доданків = {count}");
        }

        void Stoper()
        {
            Thread.Sleep(30 * 1000);  // 30 секунд
            canStop = true;
        }
    }
}
