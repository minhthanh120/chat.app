namespace chat.api.Dtos
{
    public class CommonConversationDto: ConversationDto
    {
        public string Message { get; set; }
        public string Sender { get; set; }
    }
}
