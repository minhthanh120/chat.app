using chat.api.Dtos;
using chat.api.Services.Abtractions;
using Foundation.Business.Migrations;
using Foundation.Business.Repositories.Abtractions;
using Foundation.Business.Repositories.Implements;
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
        private readonly IUserRepository _userRepository;
        public ConversationService(
            IConversationRepository conversationRepository,
            IUserConversationRepository userConversationRepository,
            IMessageRepository messageRepository,
            IUserRepository userRepository
            )
        {
            this._conversationRepository = conversationRepository;
            this._userConversationRepository = userConversationRepository;
            this._messageRepository = messageRepository;
            this._userRepository = userRepository;
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
            Conversation conversation = new Conversation() { 
                Id = message.ConversationId,
                LatestMessageAt = message.CreatedAt,
                LatestMessageId = message.Id
            };
            await _conversationRepository.Update(conversation);
        }

        public async Task<IList<CommonConversationDto>> GetLatestMessage(string userId)
        {
            var conversations = await this._userConversationRepository.ListAsync(u => u.UserId == new Guid(userId));
            IList<Guid> conversationIds = conversations.Select(s => s.ConversationId).ToList();
            var targets = await this._conversationRepository.ListAsync(c => conversationIds.Contains(c.Id));

            var latestMessages = await _messageRepository.ListAsync(m => targets.Where(x => x.LatestMessageId != null)
                .Select(x=> x.LatestMessageId).ToList().Contains(m.Id));
            var senders = await this._userRepository.ListAsync(x => latestMessages.Where(l => l.CreatedBy != new Guid(userId))
                .Select(l => l.CreatedBy).Distinct().ToList().Contains(x.Id));
            return (from c in targets join m in latestMessages on c.Id equals m.ConversationId
                    orderby (c.LatestMessageAt?? c.CreatedAt) descending
                    select new CommonConversationDto()
                    {
                        id = c.Id,
                        Name = c.Name,
                        Message = m.Text,
                        Sender = senders.FirstOrDefault(x=>x.Id == m.CreatedBy).FirstName ?? "Bạn",
                    }
                    ).ToList();
        }

        public async Task<IList<MessageDto>> GetMessages(string conversationId, string userId, int page, int limit)
        {
            var messages = await _messageRepository.ListAsync(x => x.ConversationId == new Guid(conversationId), orderBy: o => o.OrderByDescending(r => r.CreatedAt), page,limit);
            var senders = await this._userRepository.ListAsync(x => messages.Where(l => l.CreatedBy != new Guid(userId))
                .Select(l => l.CreatedBy).Distinct().ToList().Contains(x.Id));
            var result = messages.LeftJoin(senders, m => m.CreatedBy, u => u.Id, (m, u) =>
                new MessageDto()
                {
                    Message = m.Text,
                    Sender = u == null
                        ? new MemberDto()
                        : new MemberDto { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName }
                }
            );
            return result.ToList();
        }

        public async Task<IList<MemberDto>> GetMembers(string conversationId, string userId, int page, int limit)
        {
            var joined = await _userConversationRepository.ListAsync(x => x.ConversationId == new Guid(conversationId), orderBy: o => o.OrderByDescending(r => r.CreatedAt), page, limit);
            var members = await this._userRepository.ListAsync(x => joined.Select(j=>j.UserId).Contains(x.Id));
            var result = from u in members
                         select new MemberDto { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName };
            return result.ToList();
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
