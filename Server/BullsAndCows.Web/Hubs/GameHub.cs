namespace BullsAndCows.Web.Hubs
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    [HubName("game")]
    public class GameHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
        private static Dictionary<string, string> connectionMappings = new Dictionary<string, string>();

        public static void UserGameEnded(string userName)
        {
            //if (!connectionMappings.ContainsKey(userName))
            //{
            //    var connectionId = connectionMappings[userName];

            //    hubContext.Clients.Client(connectionId).test();
            //}
        }

        //public override Task OnConnected()
        //{
        //    string name = Context.User.Identity.Name;

        //    //connectionMappings.Add(name, Context.ConnectionId);

        //    return base.OnConnected();
        //}

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    string name = Context.User.Identity.Name;

        //    connectionMappings.Remove(name);

        //    return base.OnDisconnected(stopCalled);
        //}
    }
}