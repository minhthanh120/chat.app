namespace chat.api.Dtos
{
    public class MessageDto
    {
        public string Message { get; set; }
        public MemberDto Sender { get; set; }
    }
}
