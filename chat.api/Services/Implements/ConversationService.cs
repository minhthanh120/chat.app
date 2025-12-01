using chat.api.Dtos;
using chat.api.Services.Abtractions;
using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using Foundation.Models.Entities;
using System.Threading.Tasks;

namespace chat.api.Services.Implements
{
    public class ConversationService: IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserConversationRepository _userConversationRepository;
        private readonly IMessageRepository _messageRepository;
        public ConversationService(
            IConversationRepository conversationRepository,
            IUserConversationRepository userConversationRepository,
            IMessageRepository messageRepository
            )
        {
            this._conversationRepository = conversationRepository;
            this._userConversationRepository = userConversationRepository;
            this._messageRepository = messageRepository;
        }

        public async Task SendMessage(CreateMessageDto body, string userId)
        {
            var attachs = body.Attachs.Where(s=>!string.IsNullOrEmpty(s)).Distinct();
            Message message = new Message() { 
                ConversationId = body.ConversationId,
                Text = body.Message,
                CreatedBy = new Guid(userId),
                Attachs = attachs.Select(s=> new Guid(s)).ToList()
            };
            await _messageRepository.Add(message);
        }

        public async Task CreateConversation(CreateConversationDto body, string id)
        {
            try
            {
                var record = new Conversation
                {
                    Name = body.Name
                };
                var conversation = await this._conversationRepository.Add(record);
                IList<UserConversation> members = [];
                members.Add(new UserConversation
                {
                    ConversationId = conversation.Id,
                    UserId = new Guid(id)
                });
                if(body.Members != null && body.Members.Length > 0)
                {
                    foreach (var item in body.Members)
                    {
                        members.Add(new UserConversation
                        {
                            ConversationId = conversation.Id,
                            UserId = new Guid(item)
                        });
                    }
                    await this._userConversationRepository.Add(members);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 
    }
}
