namespace BullsAndCows.Data
{
    using BullsAndCows.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class BullsAndCowsDbContext : IdentityDbContext<User>
    {
        public BullsAndCowsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BullsAndCowsDbContext Create()
        {
            return new BullsAndCowsDbContext();
        }
    }
}
