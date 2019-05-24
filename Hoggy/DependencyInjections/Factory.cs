using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjections
{
    public static class Factory
    {
        public static StandardKernel Kernel;

        static Factory()
        {
            Kernel = new StandardKernel(new NinjectConfigurator());
        }
    }
}
