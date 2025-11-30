using chat.api.Dtos;
using chat.api.Services.Abtractions;
using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using System.Threading.Tasks;

namespace chat.api.Services.Implements
{
    public class ConversationService: IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserConversationRepository _userConversationRepository;
        public ConversationService(
            IConversationRepository conversationRepository,
            IUserConversationRepository userConversationRepository
            )
        {
            this._conversationRepository = conversationRepository;
            this._userConversationRepository = userConversationRepository;
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
