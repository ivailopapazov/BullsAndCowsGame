namespace BullsAndCows.Web
{
    using BullsAndCows.Data;
    using Ninject;
    using System.Data.Entity;

    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            // e.g. kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<DbContext>().To<BullsAndCowsDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
        }
    }
}
