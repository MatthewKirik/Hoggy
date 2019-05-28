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

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegistrationContractClient registrationClient = new RegistrationContractClient();
            //registrationClient.Open();
            //UserDTO newUser = new UserDTO();
            //newUser.Email = "ikirik59@gmail.com";
            //newUser.Login = "Mathhew";
            //string password = "12345678";
            //registrationClient.RegisterUser(newUser, password);
            AuthenticationContractClient authenticationClient = new AuthenticationContractClient();
            authenticationClient.Open();
            AuthenticationToken token = authenticationClient.Login("ikirik59@gmail.com", "12345678");

            DataExchangeContractClient dataExchangeClient = new DataExchangeContractClient();
            dataExchangeClient.Open();
            UserDTO me = dataExchangeClient.GetUser(token);
            Console.WriteLine(dataExchangeClient.GetBoards(token, me.Id).Length);

            while(true)
                Console.ReadLine();
        }
    }
}
