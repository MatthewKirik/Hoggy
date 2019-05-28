using Ninject;

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
