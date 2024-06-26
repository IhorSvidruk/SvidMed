﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.SignalR;
using SvidMed.Services.Data.Messages;
using SvidMed.Services.Data.Users;
using SvidMed.Web.ViewModels.MessageViewModels;

namespace SvidMed.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public ChatHub(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

         //public async Task WhoIsTyping(string name) => await Clients.Others.SendAsync("SayWhoIsTyping", name);
        public async Task SendMessage(string message, string receiverId)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var senderId = Context.User?.Identity.GetUserId();
            await _userService.GetByIdAsync<ChatUserViewModel>(senderId);

            await _messageService.CreateAsync(message, senderId, receiverId);
            await Clients.All.SendAsync(
                "ReceiveMessage",
                new ChatMessagesWithUserViewModel
                {
                    SenderId = senderId,
                    Content = message,
                });
        }
    }
}
