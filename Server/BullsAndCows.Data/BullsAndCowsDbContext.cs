namespace BullsAndCows.Data
{
    using BullsAndCows.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class BullsAndCowsDbContext : IdentityDbContext<User>
    {
        public BullsAndCowsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Game> Games { get; set; }
        public virtual IDbSet<Guess> Guesses { get; set; }

        public static BullsAndCowsDbContext Create()
        {
            return new BullsAndCowsDbContext();
        }
    }
}
