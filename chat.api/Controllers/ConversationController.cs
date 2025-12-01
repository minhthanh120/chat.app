using chat.api.Dtos;
using chat.api.Services.Abtractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chat.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConversationController : Controller
    {
        private readonly IConversationService _conversationService;
        public ConversationController(IConversationService conversationService)
        {
            this._conversationService = conversationService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateConversationDto body)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await this._conversationService.CreateConversation(body, userId);
            return Ok();
        }
        [Authorize]
        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(CreateMessageDto body)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            await this._conversationService.SendMessage(body, userId);
            return Ok();
        }
    }
}
