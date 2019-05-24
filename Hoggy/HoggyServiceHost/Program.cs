using DataAccessLayer;
using HoggyServiceHost.Helpers;
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
            var kernel = new StandardKernel(new AppBinder());
            var irep = kernel.Get<IRepository>();

            AuthenticationService service = new AuthenticationService(irep);
            ServiceHost serviceHost = new ServiceHost(service);
            serviceHost.Open();
            Console.WriteLine("Authantication server is started");
            Console.ReadLine();
        }
    }
}
