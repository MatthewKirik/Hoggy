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
    public class HoggyContextInitializer : DropCreateDatabaseIfModelChanges<HoggyContext>
    {
        protected override void Seed(HoggyContext context)
        {
        }
    }
}
