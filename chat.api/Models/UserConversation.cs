namespace chat.api.Models
{
    public class UserConversation
    {
        public Guid UserId { get; set; }
        public Guid ConversationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
