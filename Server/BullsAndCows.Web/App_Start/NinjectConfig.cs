namespace BullsAndCows.Web
{
    using BullsAndCows.Data;
    using BullsAndCows.Services;
    using BullsAndCows.Services.Contracts;
    using Ninject;
    using System.Data.Entity;

    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<BullsAndCowsDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IGameService>().To<GameService>();
        }
    }
}
