﻿using System;
using System.Threading;

namespace AppThatFailsEveryOtherTime
{
    class Program
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Simple application that fails every other time(1/2)");
            int randomNumber;
            lock (syncLock)
            {
                randomNumber = random.Next(0, 2);
            }
            if (randomNumber == 1) throw new Exception("The program fails");
            Thread.Sleep(60000);
        }
    }
}
