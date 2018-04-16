namespace BullsAndCows.Web.Hubs
{
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [Authorize]
    [HubName("game")]
    public class GameHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
        private IGameService games;

        public GameHub(IGameService games)
        {
            this.games = games;
        }

        public static void UserGameEnded(string userName)
        {
            //if (!connectionMappings.ContainsKey(userName))
            //{
            //    var connectionId = connectionMappings[userName];

            //    hubContext.Clients.Client(connectionId).test();
            //}
        }

        public void PlayComputerTurn()
        {
            string userId = this.Context.User.Identity.GetUserId();
            string userName = this.Context.User.Identity.Name;

            var newComputerGuess = this.games.MakeComputerGuess(userId);

            var result = new GuessViewModel()
            {
                Id = newComputerGuess.Id,
                Number = newComputerGuess.Number,
                BullsCount = newComputerGuess.BullsCount,
                CowsCount = newComputerGuess.CowsCount,
                DateCreated = newComputerGuess.DateCreated
            };

            this.Clients.User(userName).playComputerTurn(result);

            if (newComputerGuess.BullsCount == 4)
            {
                this.games.EndGame(userId, false);

                this.Clients.User(userName).endGame();
            }
        }
    }
}