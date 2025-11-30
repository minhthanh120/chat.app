using AutoMapper;
using chat.api.Dtos;
using Foundation.Models;
namespace chat.api.Extentions
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Conversation, ConversationDto>().ReverseMap();
        }
    }
}
