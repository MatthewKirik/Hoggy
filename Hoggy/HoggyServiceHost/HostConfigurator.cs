using DataAccessLayer;
using DependencyInjections;
using Ninject;
using System.IO;
using System.ServiceModel;
using WcfServiceLibrary.Helpers;
using WcfServiceLibrary.Interfaces;
using WcfServiceLibrary.Services;

namespace HoggyServiceHost
{
    public class HostConfigurator
    {
        private readonly IRepository _repository;
        private readonly INotificator _notificator;
        TextWriter _backlog;

        public HostConfigurator(TextWriter backlog)
        {
            _repository = Factory.Kernel.Get<IRepository>();
            _notificator = Factory.Kernel.Get<INotificator>();
            _backlog = backlog;
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

            CreationService creationService = new CreationService(_repository, _notificator);
            ServiceHost creationServiceHost = new ServiceHost(creationService);

            CommunityService communityService = new CommunityService(_repository, _notificator);
            ServiceHost communityServiceHost = new ServiceHost(communityService);

            NotificationService notificactionService = new NotificationService(_repository, _notificator);
            ServiceHost notificactionServiceHost = new ServiceHost(notificactionService);

            authServiceHost.Open();
            _backlog.WriteLine("Authantication service is started");
            regServiceHost.Open();
            _backlog.WriteLine("Registration service is started");
            dataExServiceHost.Open();
            _backlog.WriteLine("Data exchange service is started");
            creationServiceHost.Open();
            _backlog.WriteLine("Creation service is started");
            communityServiceHost.Open();
            _backlog.WriteLine("Community service is started");
            notificactionServiceHost.Open();
            _backlog.WriteLine("Notificaction service is started");
        }
    }
}
