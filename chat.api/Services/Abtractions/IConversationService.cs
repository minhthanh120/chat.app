using chat.api.Dtos;

namespace chat.api.Services.Abtractions
{
    public interface IConversationService
    {
        Task CreateConversation(CreateConversationDto body, string id);
        Task SendMessage(CreateMessageDto body, string userId);
    }
}
