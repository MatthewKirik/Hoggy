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
    public class HoggyContextInitializer : DropCreateDatabaseAlways<HoggyContext>
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
            context.SaveChanges();


            BoardEntity admin1Board1 = new BoardEntity
            {
                Name = "Test",
                CreationDate = DateTime.Now,
                Creator = context.Users.First(x => x.Login == "admin1"),
                Description = "test board"
            };

            context.Boards.Add(admin1Board1);
            context.SaveChanges();

            ColumnEntity admin1Board1Column1 = new ColumnEntity
            {
                Name = "TestCol",
                Board = context.Boards.First(x => x.Name == "Test"),
                Description = "test board"
            };

            context.Columns.Add(admin1Board1Column1);
            context.SaveChanges();

            CardEntity card = new CardEntity
            {
                Name = "first card",
                CreationDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(1),
                Description = "first test card",
                Column = context.Columns.First(x => x.Name == "TestCol")
            };

            context.Cards.Add(card);
            context.SaveChanges();

            BoardEntity b = context.Boards.First(x => x.Name == "Test");
            ColumnEntity col = context.Columns.First(x => x.Name == "TestCol");
            CardEntity c = context.Cards.First(x => x.Name == "first card");

            b.Columns = new List<ColumnEntity>() { col };
            col.Cards = new List<CardEntity>() { c };
            context.SaveChanges();
        }
    }
}
