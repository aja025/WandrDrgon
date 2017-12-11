using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using System;

namespace WanderDragon.Hubs
{
    [Authorize]
    public class MultiPlayer : Hub
    {
        public static class UserHandler
        {
            public static HashSet<string> ConnectedIds = new HashSet<string>();
        }
        public Task All(int col, int row)
        {

            return Clients.All.InvokeAsync("All", col, row);
        }
        public Task Send(string name, string message)
        {
            return Clients.All.InvokeAsync("Send", $"{name}:  {message}");
        }
        public Task Count()
        {
            return Clients.All.InvokeAsync( "Count", UserHandler.ConnectedIds.Count);
        }
        public Task Joined(string name){

            bool there = false;

            foreach (var item in UserHandler.ConnectedIds)
            {
                if(Context.ConnectionId == item)
                {
                    there = true;
                }
            }
            if(there == true){
                return Clients.All.InvokeAsync("Joined", $"{name} has Joined the game");
            }
            else
            {
                return Clients.All.InvokeAsync("Joined", $"{name} has Left the game");                
            }
            
        }
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

    }
}