using DataAccessLayer;
using DataAccessLayer.Entities;
using DataTransferObjects;
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
            IRepository repository = Factory.Kernel.Get<IRepository>();

            AuthenticationService authService = new AuthenticationService(repository);
            ServiceHost authServiceHost = new ServiceHost(authService);

            RegistrationService regService = new RegistrationService(repository);
            ServiceHost regServiceHost = new ServiceHost(regService);

            DataExchangeService dataExService = new DataExchangeService(repository);
            ServiceHost dataExServiceHost = new ServiceHost(dataExService);


            authServiceHost.Open();
            Console.WriteLine("Authantication server is started");
            regServiceHost.Open();
            Console.WriteLine("Registration server is started");
            dataExServiceHost.Open();
            Console.WriteLine("Data exchange server is started");

            Console.ReadLine();
        }
    }
}
