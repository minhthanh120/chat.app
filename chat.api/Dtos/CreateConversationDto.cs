namespace chat.api.Dtos
{
    public class CreateConversationDto
    {
        public string Name { get; set; }
        public string[]? Members { get; set; }
    }
}
