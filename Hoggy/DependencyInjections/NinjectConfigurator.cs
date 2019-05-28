using DataAccessLayer;
using DataAccessLayer.Repositories;
using Ninject.Modules;
using System.Data.Entity;

namespace DependencyInjections
{
    internal class NinjectConfigurator : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<HoggyContext>();
            Bind<IRepository>().To<MsSqlRepository>();
        }
    }
}
