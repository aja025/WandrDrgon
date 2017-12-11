using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WanderDragon.Hubs
{
    public class Chat : Hub
    {
        public Task Send(string name, string message)
        {
            return Clients.All.InvokeAsync("Send",$"{name} : {message}" );
        }
    }
}