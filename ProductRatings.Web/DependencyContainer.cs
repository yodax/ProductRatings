using Autofac;
using Autofac.Integration.Mvc;
using ProductRatings.Persistence;

namespace ProductRatings.Web
{
    public static class DependencyContainer
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(c => new DatabaseBackend(@"c:\temp\database.bin")).As<IPersistenceBackend>();
            builder.RegisterType<Catalog>();

            return builder.Build();
        }

        public static AutofacDependencyResolver MvcDiResolver()
        {
            return new AutofacDependencyResolver(Build());
        }
    }
}