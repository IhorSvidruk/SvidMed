using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Messages;
using SvidMed.Web.ViewModels.MessageViewModels;

namespace SvidMed.Web.ViewComponents
{
    [ViewComponent(Name = "ChatConversations")]
    public class ChatConversationsViewComponent : ViewComponent
    {
        private readonly IMessageService _messageService;

        public ChatConversationsViewComponent(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUserId = User.Identity.GetUserId();
            var conversations = await _messageService.GetAllAsync<ChatConversationsViewModel>(currentUserId);

            foreach (var user in conversations)
            {
                user.LastMessage = await _messageService.GetLastMessageAsync(currentUserId, user.Id);
                user.LastMessageActivity = await _messageService.GetLastActivityAsync(currentUserId, user.Id);
            }

            return View(conversations);
        }
    }
}
