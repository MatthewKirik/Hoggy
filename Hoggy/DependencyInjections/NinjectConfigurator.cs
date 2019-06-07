using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Ninject.Modules;
using System.Data.Entity;
using WcfServiceLibrary.Interfaces;
using WcfServiceLibrary.Logic;

namespace DependencyInjections
{
    internal class NinjectConfigurator : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<HoggyContext>();
            Bind<IRepository>().To<MsSqlRepository>();
            Bind<INotificator>().To<Notificator>();
            Bind<IFileRepository>().To<FileRepository>();
        }
    }
}
