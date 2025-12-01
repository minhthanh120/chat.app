using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Models.Entities
{
    public class Message
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("conversation_id")]
        public Guid ConversationId { get; set; }
        [BsonElement("created_by")]
        public Guid CreatedBy { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("attachs")]
        public IList<Guid>? Attachs { get; set; }
    }
}
