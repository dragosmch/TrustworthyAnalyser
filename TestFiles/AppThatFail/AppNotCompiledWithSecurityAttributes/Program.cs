using System;
using System.Threading;

namespace AppNotCompiledWithSecurityAttributes
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 10);
            Thread.Sleep(randomNumber * 10000);
        }
    }
}
