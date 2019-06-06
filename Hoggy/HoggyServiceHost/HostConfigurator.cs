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
            authServiceHost.Open();
            _backlog.WriteLine("Authantication service is started");

            RegistrationService regService = new RegistrationService(_repository);
            ServiceHost regServiceHost = new ServiceHost(regService);
            regServiceHost.Open();
            _backlog.WriteLine("Registration service is started");

            DataExchangeService dataExService = new DataExchangeService(_repository);
            ServiceHost dataExServiceHost = new ServiceHost(dataExService);
            dataExServiceHost.Open();
            _backlog.WriteLine("Data exchange service is started");

            CreationService creationService = new CreationService(_repository, _notificator);
            ServiceHost creationServiceHost = new ServiceHost(creationService);
            creationServiceHost.Open();
            _backlog.WriteLine("Creation service is started");

            CommunityService communityService = new CommunityService(_repository, _notificator);
            ServiceHost communityServiceHost = new ServiceHost(communityService);
            communityServiceHost.Open();
            _backlog.WriteLine("Community service is started");

            DeletionService deletionService = new DeletionService(_repository, _notificator);
            ServiceHost deletionServiceHost = new ServiceHost(deletionService);
            deletionServiceHost.Open();
            _backlog.WriteLine("Deletion service is started");

            NotificationService notificactionService = new NotificationService(_repository, _notificator);
            ServiceHost notificactionServiceHost = new ServiceHost(notificactionService);
            notificactionServiceHost.Open();
            _backlog.WriteLine("Notificaction service is started");

            EditionService editionService = new EditionService(_repository, _notificator);
            ServiceHost editionServiceHost = new ServiceHost(editionService);
            editionServiceHost.Open();
            _backlog.WriteLine("Edition service is started");
        }
    }
}