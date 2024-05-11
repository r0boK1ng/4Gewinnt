﻿using Microsoft.AspNetCore.SignalR;
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Protocol;
using System.Text;
using VierGewinnt.Data.Interfaces;

namespace VierGewinnt.Hubs
{
    public class PlayerlobbyHub : Hub
    {
        static readonly IList<string> players = new List<string>();
        static readonly IDictionary<string, string> onlineUsers = new Dictionary<string, string>();

        public async Task SendNotification(string player)
        {
            await Clients.Others.SendAsync("ReceiveNewUser", player);
        }

        public async Task AddUser(string player)
        {
            if (players.Contains(player))
            {
                return;
            }
            else
            {
                onlineUsers.Add(player, Context.ConnectionId);
                players.Add(player);
            }
            return;
        }

        public async Task GetAvailableUsers()
        {
            await Clients.Caller.SendAsync("ReceiveAvailableUsers", players);
        }

        public async Task LeaveLobby(string userName)
        {
            if (players.Contains(userName))
            {
                players.Remove(userName);
                onlineUsers.Remove(userName);
                await Clients.Others.SendAsync("PlayerLeft", userName);
                return;
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userName) && players.Contains(userName))
            {
                players.Remove(userName);
                onlineUsers.Remove(userName);
                await Clients.Others.SendAsync("PlayerLeft", userName);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
