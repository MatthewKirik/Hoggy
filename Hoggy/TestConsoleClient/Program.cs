using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TestConsoleClient.AuthenticationService;
using TestConsoleClient.RegistationService;
using DataTransferObjects;
using TestConsoleClient.DataExchangeService;
using TestConsoleClient.CreationService;

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();
            generator.InitializeHierarchy();


            while (true)
                Console.ReadLine();
        }
    }
}
