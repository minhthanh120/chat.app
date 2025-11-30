using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Business.Repositories.Implements
{
    public class UserConversationRepository:BaseRepository<UserConversation>, IUserConversationRepository
    {
        public UserConversationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
