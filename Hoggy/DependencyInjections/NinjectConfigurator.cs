using DataAccessLayer;
using DataAccessLayer.Repositories;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
