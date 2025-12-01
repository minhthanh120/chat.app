namespace chat.api.Dtos
{
    public class CreateMessageDto
    {
        public Guid ConversationId { get; set; }
        public string Message { get; set; }
        public string[]? Attachs { get; set; }
    }
}
