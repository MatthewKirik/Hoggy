using System.Data.Entity;

namespace DataAccessLayer
{
    //public class HoggyContextInitializer : DropCreateDatabaseIfModelChanges<HoggyContext>
    public class HoggyContextInitializer : DropCreateDatabaseAlways<HoggyContext>
    //public class HoggyContextInitializer : CreateDatabaseIfNotExists<HoggyContext>
    {
        public override void InitializeDatabase(HoggyContext context)
        {
            if(context.Database.Exists())
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
    , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }
        protected override void Seed(HoggyContext context)
        {

        }
    }
}
