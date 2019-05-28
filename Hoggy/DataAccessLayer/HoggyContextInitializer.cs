﻿using System.Data.Entity;

namespace DataAccessLayer
{
    public class HoggyContextInitializer : DropCreateDatabaseAlways<HoggyContext>
    {
        public override void InitializeDatabase(HoggyContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
    , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }
        protected override void Seed(HoggyContext context)
        {
        }
    }
}
