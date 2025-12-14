using chat.api.Dtos;

namespace chat.api.Services.Abtractions
{
    public interface IConversationService
    {
        Task CreateConversation(CreateConversationDto body, string id);
        Task SendMessage(CreateMessageDto body, string userId);
        Task<IList<CommonConversationDto>> GetLatestMessage(string userId);
        Task<IList<MessageDto>> GetMessages(string conversationId, string userId, int page, int limit);
        Task<IList<MemberDto>> GetMembers(string conversationId, string userId, int page, int limit);

    }
}
