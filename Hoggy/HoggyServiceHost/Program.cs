using System;
using System.IO;

namespace HoggyServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            HostConfigurator configurator = new HostConfigurator(Console.Out);
            configurator.Start();
            while (true)
                Console.ReadLine();

        }
    }
}
