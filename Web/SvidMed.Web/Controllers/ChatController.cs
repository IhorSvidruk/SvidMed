using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Messages;
using SvidMed.Services.Data.Users;
using SvidMed.Web.ViewModels.MessageViewModels;

namespace SvidMed.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public ChatController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public IActionResult All() => View();

        public async Task<IActionResult> SendMessage()
        {
            var viewModel = new ChatSendMessageInputModel
            {
                Users = await _userService.GetAllAsync<ChatUserViewModel>(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatSendMessageInputModel input)
        {
            await _messageService.CreateAsync(input.Message, User.FindFirstValue(ClaimTypes.NameIdentifier), input.ReceiverId);

            return RedirectToAction(nameof(WithUser), new { id = input.ReceiverId });
        }

        public async Task<IActionResult> WithUser(string id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new ChatWithUserViewModel
            {
                User = await _userService.GetByIdAsync<ChatUserViewModel>(id),
                Messages = await _messageService.GetAllWithUserAsync<ChatMessagesWithUserViewModel>(currentUserId, id),
            };

            return View(viewModel);
        }
    }
}
