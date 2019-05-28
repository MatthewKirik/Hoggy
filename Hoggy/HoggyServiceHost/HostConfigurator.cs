using DataAccessLayer;
using DependencyInjections;
using Ninject;
using System;
using System.IO;
using System.ServiceModel;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Services;

namespace HoggyServiceHost
{
    public class HostConfigurator
    {
        IRepository _repository;
        TextWriter backlog;

        public HostConfigurator(TextWriter backlog)
        {
            _repository = Factory.Kernel.Get<IRepository>();
            MapperConfigurator.Configure();
        }

        public void Start()
        {
            AuthenticationService authService = new AuthenticationService(_repository);
            ServiceHost authServiceHost = new ServiceHost(authService);

            RegistrationService regService = new RegistrationService(_repository);
            ServiceHost regServiceHost = new ServiceHost(regService);

            DataExchangeService dataExService = new DataExchangeService(_repository);
            ServiceHost dataExServiceHost = new ServiceHost(dataExService);

            CreationService creationService = new CreationService(_repository);
            ServiceHost creationServiceHost = new ServiceHost(creationService);

            CommunityService communityService = new CommunityService(_repository);
            ServiceHost communityServiceHost = new ServiceHost(communityService);


            authServiceHost.Open();
            backlog.WriteLine("Authantication service is started");
            regServiceHost.Open();
            backlog.WriteLine("Registration service is started");
            dataExServiceHost.Open();
            backlog.WriteLine("Data exchange service is started");
            creationServiceHost.Open();
            backlog.WriteLine("Creation service is started");
            communityServiceHost.Open();
            backlog.WriteLine("Community service is started");
        }
    }
}
