namespace BullsAndCows.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<Game> games;
        private ICollection<Guess> guesses;

        public User()
        {
            this.games = new HashSet<Game>();
            this.guesses = new HashSet<Guess>();
        }

        public virtual ICollection<Game> Games
        {
            get { return this.games; }
            set { this.games = value; }
        }

        public virtual ICollection<Guess> Guesses
        {
            get { return guesses; }
            set { guesses = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
