namespace chat.api.Dtos
{
    public class MemberDto: UserDto
    {
        public DateTime JoinedAt { get; set; }
    }
}
