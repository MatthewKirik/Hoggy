﻿using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using TestConsoleClient.AuthenticationService;
using TestConsoleClient.CommunityService;
using TestConsoleClient.CreationService;
using TestConsoleClient.DataExchangeService;
using TestConsoleClient.DeletionService;
using TestConsoleClient.EditionService;
using TestConsoleClient.FileExchangeService;
using TestConsoleClient.NotificationService;
using TestConsoleClient.RegistationService;

namespace TestConsoleClient
{
    public class Generator
    {
        DataExchangeContractClient dataExchangeClient;
        RegistrationContractClient registrationClient;
        AuthenticationContractClient authenticationClient;
        CreationContractClient creationClient;
        NotificationContractClient notificationClient;
        CommunityContractClient communityClient;
        EditionContractClient editionClient;
        DeletionContractClient deletionClient;
        FileExchangeContractClient fileExchangeClient;

        public Generator()
        {
            dataExchangeClient = new DataExchangeContractClient();
            dataExchangeClient.Open();
            registrationClient = new RegistrationContractClient();
            registrationClient.Open();
            authenticationClient = new AuthenticationContractClient();
            authenticationClient.Open();
            creationClient = new CreationContractClient();
            creationClient.Open();
            communityClient = new CommunityContractClient();
            communityClient.Open();
            editionClient = new EditionContractClient();
            editionClient.Open();
            deletionClient = new DeletionContractClient();
            deletionClient.Open();
            fileExchangeClient = new FileExchangeContractClient();
            fileExchangeClient.Open();
            notificationClient = new NotificationContractClient(new InstanceContext(new NotificationCallbackHandler()));
            notificationClient.Open();
        }

        public void TestFiles()
        {
            AuthenticationToken token = RegisterAndLoginUsers(1)[0];
            Stream stream = File.OpenRead("img.png");
            AddImageRequestMessage requestMessage = new AddImageRequestMessage()
            {
                FileByteStream = stream,
                Token = token,
                Length = stream.Length
            };
            fileExchangeClient.SetUserProfileImage(requestMessage);
            GetImageRequestMessage getImgRequest = new GetImageRequestMessage()
            {
                Token = token, 
                UserId = 1
            };
            GetImageResponseMessage response = fileExchangeClient.GetUserProfileImage(getImgRequest);
            using (FileStream outStream = File.Open("inImg.png", FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[2048];
                int readBytes = 0;
                do
                {
                    readBytes = response.FileByteStream.Read(buffer, 0, buffer.Length);
                    outStream.Write(buffer, 0, readBytes);
                } while (outStream.Length < response.Length);
            }
        }

        public void CreateGroup(int participantsAmount)
        {
            List<AuthenticationToken> peopleTokens = RegisterAndLoginUsers(participantsAmount);
            AuthenticationToken mainToken = peopleTokens[0];
            UserDTO mainUser = dataExchangeClient.GetUser(mainToken);
            peopleTokens.RemoveAt(0);
            List<UserDTO> people = new List<UserDTO>();
            foreach (var token in peopleTokens)
            {
                people.Add(dataExchangeClient.GetUser(token));
            }
            AddBoards(mainToken, 1);
            BoardDTO board = dataExchangeClient.GetBoards(mainToken, mainUser.Id)[0];
            //notificationClient = new NotificationContractClient(new InstanceContext(new NotificationCallbackHandler()));
            //notificationClient.Open();
            //notificationClient.Subscribe(mainToken, board.Id);
            Task.Run(() =>
            {
                for (int i = 0; i < people.Count; i++)
                {
                    communityClient.SendInvitation(mainToken, board.Id, people[i].Email);
                    InvitationDTO invitation = dataExchangeClient.GetIncomeInvitations(peopleTokens[i], people[i].Id)[0];
                    communityClient.AcceptInvitation(peopleTokens[i], invitation.Key);
                    //notificationClient.Subscribe(peopleTokens[i], board.Id);
                }
                AddColumns(mainToken, board.Id, 5);
            });


        }

        public void TestInvitations()
        {
            List<AuthenticationToken> tokens = RegisterAndLoginUsers(2);
            UserDTO mainUser = dataExchangeClient.GetUser(tokens[0]);
            UserDTO subUser = dataExchangeClient.GetUser(tokens[1]);
            AddBoards(tokens[0], 1);
            BoardDTO board = dataExchangeClient.GetBoards(tokens[0], mainUser.Id)[0];
            communityClient.SendInvitation(tokens[0], board.Id, subUser.Email);
            InvitationDTO invitation = dataExchangeClient.GetIncomeInvitations(tokens[1], subUser.Id)[0];
            communityClient.AcceptInvitation(tokens[1], invitation.Key);
            Console.WriteLine(dataExchangeClient.GetParticipatedBoards(tokens[1], subUser.Id)[0].Name);
        }

        public void EditionTest()
        {
            AuthenticationToken token = RegisterAndLoginUsers(1)[0];
            UserDTO user = dataExchangeClient.GetUser(token);
            AddBoards(token, 1);
            BoardDTO board = dataExchangeClient.GetBoards(token, user.Id)[0];
            AddColumns(token, board.Id, 1);
            ColumnDTO column = dataExchangeClient.GetColumns(token, board.Id)[0];
            column.Description = "Haha";
            Console.WriteLine(editionClient.EditColumn(token, column).ToString());
            column.Description = "Hehe";
            Console.WriteLine(editionClient.EditColumn(token, column).ToString());
        }

        public void DeletionTest(int userAmount, int boardAmount, int tagAmount, int columnAmount, int cardAmount)
        {
            List<AuthenticationToken> tokens = RegisterAndLoginUsers(userAmount);
            List<UserDTO> users = new List<UserDTO>();
            foreach (var token in tokens)
            {
                users.Add(dataExchangeClient.GetUser(token));
            }
            for (int i = 0; i < userAmount; i++)
            {
                AddBoards(tokens[i], boardAmount);
                List<BoardDTO> boards = dataExchangeClient.GetBoards(tokens[i], users[i].Id).ToList();
                foreach (var b in boards)
                {
                    AddTagsToBoard(tokens[i], b.Id, tagAmount);
                    AddColumns(tokens[i], b.Id, columnAmount);
                    List<ColumnDTO> columns = dataExchangeClient.GetColumns(tokens[i], b.Id).ToList();
                    foreach (var c in columns)
                    {
                        AddCards(tokens[i], c.Id, cardAmount);
                        List<CardDTO> cards = dataExchangeClient.GetCards(tokens[i], c.Id).ToList();
                        //foreach (var card in cards)
                        //{
                        //    bool delete = deletionClient.DeleteCard(tokens[i], card.Id);
                        //    Console.WriteLine(delete.ToString());
                        //}
                    }
                    //foreach (var c in columns)
                    //{
                    //    bool delete = deletionClient.DeleteColumn(tokens[i], c.Id);
                    //    Console.WriteLine(delete.ToString());
                    //}
                }
            }
        }

        public void TeamHierarchy(int userAmount, int boardAmount, int tagAmount, int columnAmount, int cardAmount)
        {
            List<AuthenticationToken> tokens = RegisterAndLoginUsers(userAmount);
            List<UserDTO> users = new List<UserDTO>();
            foreach (var token in tokens)
            {
                users.Add(dataExchangeClient.GetUser(token));
            }
            AddBoards(tokens[0], boardAmount);
            List<BoardDTO> boards = dataExchangeClient.GetBoards(tokens[0], users[0].Id).ToList();
            foreach (var b in boards)
            {
                //notificationClient.Subscribe(tokens[i], b.Id);
                AddTagsToBoard(tokens[0], b.Id, tagAmount);
                AddColumns(tokens[0], b.Id, columnAmount);
                List<ColumnDTO> columns = dataExchangeClient.GetColumns(tokens[0], b.Id).ToList();
                foreach (var c in columns)
                {
                    AddCards(tokens[0], c.Id, cardAmount);
                }

                for (int i = 1; i < tokens.Count; i++)
                {
                    communityClient.SendInvitation(tokens[0], b.Id, users[i].Email);
                    InvitationDTO invitation = dataExchangeClient.GetIncomeInvitations(tokens[i], users[i].Id)[0];
                    bool isAccepted = communityClient.AcceptInvitation(tokens[i], invitation.Key);
                    Console.WriteLine("User accepting invitation. Rezult: " + isAccepted.ToString());
                }
            }
        }

        public void InitializeHierarchy(int userAmount, int boardAmount, int tagAmount, int columnAmount, int cardAmount)
        {
            List<AuthenticationToken> tokens = RegisterAndLoginUsers(userAmount);
            List<UserDTO> users = new List<UserDTO>();
            foreach (var token in tokens)
            {
                users.Add(dataExchangeClient.GetUser(token));
            }
            for (int i = 0; i < userAmount; i++)
            {
                AddBoards(tokens[i], boardAmount);
                List<BoardDTO> boards = dataExchangeClient.GetBoards(tokens[i], users[i].Id).ToList();
                foreach (var b in boards)
                {
                    //notificationClient.Subscribe(tokens[i], b.Id);
                    AddTagsToBoard(tokens[i], b.Id, tagAmount);
                    AddColumns(tokens[i], b.Id, columnAmount);
                    List<ColumnDTO> columns = dataExchangeClient.GetColumns(tokens[i], b.Id).ToList();
                    foreach (var c in columns)
                    {
                        AddCards(tokens[i], c.Id, cardAmount);
                    }
                }
            }
        }

        public List<AuthenticationToken> RegisterAndLoginUsers(int amount)
        {
            List<AuthenticationToken> loggedUsers = new List<AuthenticationToken>();
            for (int i = 0; i < amount; i++)
            {
                UserDTO newUser = new UserDTO
                {
                    Email = "user" + (i + 1).ToString() + "@gmail.com",
                    Login = "user" + (i + 1).ToString()
                };
                registrationClient.RegisterUser(newUser, "user" + (i + 1).ToString());
                loggedUsers.Add(authenticationClient.Login(newUser.Email, "user" + (i + 1).ToString()));
                Console.WriteLine("User " + (i + 1).ToString() + " registered");
            }
            return loggedUsers;
        }

        public void AddBoards(AuthenticationToken token, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                BoardDTO board = new BoardDTO
                {
                    CreationDate = DateTime.Now,
                    Description = "Autogenerated board number " + (i + 1).ToString(),
                    Name = "TestBoard" + (i + 1).ToString()
                };
                creationClient.AddBoard(token, board);
                Console.WriteLine("Board " + (i + 1).ToString() + " added");
            }
        }

        public void AddColumns(AuthenticationToken token, int boardId, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                ColumnDTO column = new ColumnDTO
                {
                    Description = "Autogenerated column number " + (i + 1).ToString(),
                    Name = "TestColumn" + (i + 1).ToString()
                };
                creationClient.AddColumn(token, column, boardId);
                Console.WriteLine("Column " + (i + 1).ToString() + " added");
            }
        }

        public void AddTagsToBoard(AuthenticationToken token, int boardId, int amount)
        {
            Random rand = new Random();
            for (int i = 0; i < amount; i++)
            {
                TagDTO tag = new TagDTO
                {
                    Name = "SuperTag" + (i + 1).ToString(),
                    Color = "#" + rand.Next(0, 256).ToString("X2") + rand.Next(0, 256).ToString("X2") + rand.Next(0, 256).ToString("X2")
                };
                creationClient.AddTagToBoard(token, tag, boardId);
                Console.WriteLine("Tag " + (i + 1).ToString() + " added");
            }
        }

        public void AddCards(AuthenticationToken token, int columnId, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CardDTO card = new CardDTO
                {
                    ExpireDate = DateTime.Now.AddDays(2),
                    CreationDate = DateTime.Now,
                    Description = "Autogenerated card number " + (i + 1).ToString(),
                    Name = "TestCard" + (i + 1).ToString()
                };
                creationClient.AddCard(token, card, columnId);
                Console.WriteLine("Card " + (i + 1).ToString() + " added");
            }
        }
    }
}
