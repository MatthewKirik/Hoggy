using DataAccessLayer;
using DependencyInjections;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.Services;

namespace HoggyServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthenticationService service = new AuthenticationService(Factory.Kernel.Get<IRepository>());
            ServiceHost serviceHost = new ServiceHost(service);
            serviceHost.Open();
            Console.WriteLine("Authantication server is started");
            Console.ReadLine();
        }
    }
}
