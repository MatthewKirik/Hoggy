using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataAccessLayer
{
    public class HoggyContextInitializer : CreateDatabaseIfNotExists<HoggyContext>
    {
        protected override void Seed(HoggyContext context)
        {
            SHA256Managed encryptor = new SHA256Managed();
            UserEntity admin1 = new UserEntity
            {
                Login = "admin1",
                Email = "ikirik59@gmail.com",
                Password = BitConverter.ToString(encryptor.ComputeHash(Encoding.Unicode.GetBytes("admin"))),
                FirstName = "Matthew",
                SecondName = "Kirik"
            };

            context.Users.Add(admin1);

            BoardEntity admin1Board1 = new BoardEntity
            {
                Name = "Test",
                CreationDate = DateTime.Now,
                Creator = context.Users.First(x => x.Login == "admin1"),
                Description = "test board"
            };

            context.Boards.Add(admin1Board1);

            CardEntity card = new CardEntity
            {
                Name = "first card",
                CreationDate = DateTime.Now,
                Description = "first test card"
            };

            context.Cards.Add(card);

            context.Boards.First(x => x.Name == "Test").Cards
                .Add(context.Cards.First(x => x.Name == "first card"));

        }
    }
}
