using Foundation.Business.Repositories.Abtractions;
using Foundation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Business.Repositories.Implements
{
    public class MessageRepository:MongoBaseRepository<Message>,IMessageRepository
    {
        public MessageRepository(MongoDbContext context) :base(context)
        {
        }
    }
}
