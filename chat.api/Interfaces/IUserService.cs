using chat.api.Dtos;

namespace chat.api.Interfaces
{
    public interface IUserService
    {
        UserDto Get(string id);
        bool Create(CreateUserDto body);
    }
}
