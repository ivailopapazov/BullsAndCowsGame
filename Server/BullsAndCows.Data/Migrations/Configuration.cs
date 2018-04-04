namespace BullsAndCows.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BullsAndCowsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "BullsAndCows.Data.BullsAndCowsDbContext";
        }

        protected override void Seed(BullsAndCowsDbContext context)
        {
        }
    }
}
