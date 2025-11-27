using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foundation.Models
{
    [Table("user_conversation")]
    [PrimaryKey(nameof(UserId), nameof(ConversationId))]
    public class UserConversation
    {
        public Guid UserId { get; set; }
        public Guid ConversationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
