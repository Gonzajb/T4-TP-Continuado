using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TallerIV.SignalRHubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string encuentro_id)
        {
            // Call the broadcastMessage method to update clients.
            Clients.Group(encuentro_id).addChatMessage(name, message);
        }
        public void JoinRoom(string encuentro_id)
        {
            Groups.Add(Context.ConnectionId, encuentro_id);
        }

        public void LeaveRoom(string encuentro_id)
        {
            Groups.Remove(Context.ConnectionId, encuentro_id);
        }
    }
}