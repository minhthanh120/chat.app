using chat.api.Dtos;

namespace chat.api.Services.Abtractions
{
    public interface IUserService
    {
        Task<UserDto> Register(CreateUserDto body);
        Task<LoginResponse> Login(LoginDto body);
        Task<UserDto> GetUserById(string id);
    }
}
