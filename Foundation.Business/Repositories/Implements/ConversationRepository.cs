using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Business.Repositories.Implements
{
    public class ConversationRepository:BaseRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ApplicationDbContext context): base(context)
        {
            
        }
    }
}
