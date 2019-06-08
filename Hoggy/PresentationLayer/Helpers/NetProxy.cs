using DataTransferObjects;
using PresentationLayer.AuthenticationService;
using PresentationLayer.CommunityService;
using PresentationLayer.CreationService;
using PresentationLayer.DataExchangeService;
using PresentationLayer.DeletionService;
using PresentationLayer.EditionService;
using PresentationLayer.NotificationService;
using PresentationLayer.RegistrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Helpers
{
    public static class NetProxy
    {
        static AuthenticationToken _token;
        public static AuthenticationToken Token { get => _token; }
        
        static AuthenticationContractClient _authProxy;
        public static AuthenticationContractClient AuthProxy { get => _authProxy; }

        static CommunityContractClient _commProxy;
        public static CommunityContractClient CommProxy { get => _commProxy; }
        
        static CreationContractClient _creationProxy;
        public static CreationContractClient CreationProxy { get => _creationProxy; }

        static DataExchangeContractClient _dataExchangeProxy;
        public static DataExchangeContractClient DataExchProxy { get => _dataExchangeProxy; }

        static DeletionContractClient _deletionProxy;
        public static DeletionContractClient DeletionProxy { get => _deletionProxy; }

        static EditionContractClient _editionProxy;
        public static EditionContractClient EditionProxy { get => _editionProxy; }

        static NotificationContractClient _notificationProxy;
        public static NotificationContractClient NotificationProxy { get => _notificationProxy; }

        static RegistrationContractClient _regProxy;
        public static RegistrationContractClient RegProxy { get => _regProxy; }

        static CallbackHandler _callbackHandler;
        public static CallbackHandler CallbackHandler { get => _callbackHandler; }
        
        public static void Configure()
        {
            _callbackHandler = new CallbackHandler();
            _authProxy = new AuthenticationContractClient();
            _authProxy.Open();

            _commProxy = new CommunityContractClient();
            _commProxy.Open();

            _creationProxy = new CreationContractClient();
            _creationProxy.Open();

            _dataExchangeProxy = new DataExchangeContractClient();
            _dataExchangeProxy.Open();

            _deletionProxy = new DeletionContractClient();
            _deletionProxy.Open();

            _editionProxy = new EditionContractClient();
            _editionProxy.Open();

            _notificationProxy = new NotificationContractClient(new InstanceContext(new CallbackHandler()));
            _notificationProxy.Open();

            _regProxy = new RegistrationContractClient();
            _regProxy.Open();
        }

        public static void SetToken(AuthenticationToken token)
        {
            _token = token; 
        }
    }
}
