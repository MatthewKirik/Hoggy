using DataAccessLayer;
using DataAccessLayer.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoggyServiceHost.Helpers
{
    public class AppBinder : NinjectModule
    {
        //Bind(typeof(IRepository<>)).To(typeof(Repository<>)).WithConstructorArgument("_context", new DataAccessLayer.AppContext());
        public override void Load()
        {
            Bind(typeof(IRepository)).To(typeof(MsSqlRepository)).WithConstructorArgument("ctx", new DataAccessLayer.HoggyContext());
        }
    }
}
