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
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Services;

namespace HoggyServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            MapperConfigurator.Configure();

            IRepository repository = Factory.Kernel.Get<IRepository>();

            AuthenticationService authService = new AuthenticationService(repository);
            ServiceHost authServiceHost = new ServiceHost(authService);

            RegistrationService regService = new RegistrationService(repository);
            ServiceHost regServiceHost = new ServiceHost(regService);

            DataExchangeService dataExService = new DataExchangeService(repository);
            ServiceHost dataExServiceHost = new ServiceHost(dataExService);

            CreationService creationService = new CreationService(repository);
            ServiceHost creationServiceHost = new ServiceHost(creationService);

            CommunityService communityService = new CommunityService(repository);
            ServiceHost communityServiceHost = new ServiceHost(communityService);


            authServiceHost.Open();
            Console.WriteLine("Authantication service is started");
            regServiceHost.Open();
            Console.WriteLine("Registration service is started");
            dataExServiceHost.Open();
            Console.WriteLine("Data exchange service is started");
            creationServiceHost.Open();
            Console.WriteLine("Creation service is started");
            communityServiceHost.Open();
            Console.WriteLine("Community service is started");

            Console.ReadLine();
        }
    }
}
