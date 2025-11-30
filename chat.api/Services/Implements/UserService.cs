using AutoMapper;
using chat.api.Dtos;
using chat.api.Services.Abtractions;
using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using Foundation.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chat.api.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IMapper _mapper;
        private const int KeySize = 32;
        private const int Iterations = 1000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository,
            IConfiguration configuration,
            IMapper mapper,
            IUserCredentialRepository userCredentialRepository) { 
            _userRepository = userRepository;
            _userCredentialRepository = userCredentialRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                User data = await this._userRepository.GetById(new Guid(id));
                var user = _mapper.Map<UserDto>(data);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<UserDto> Register(CreateUserDto createUser)
        {
            try
            {
                var entity = new User
                {
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    Email = createUser.Email,
                    Phone = createUser.Phone,
                };
                var user = await this._userRepository.Add(entity);
                byte[] salt = RandomNumberGenerator.GetBytes(16);
                var cred = new UserCredentials
                {
                    Guid = user.Id,
                    PasswordSalt = salt,
                    PasswordHash = this.HashPassword(salt, createUser.Password)
                };
                await this._userCredentialRepository.Add(cred);
                return new UserDto { Id = user.Id,
                    FirstName = createUser.FirstName,
                        LastName = createUser.LastName,
                        Email = createUser.Email,
                        Phone = createUser.Phone,
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string HashPassword(byte[] salt, string password)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                _hashAlgorithm,
                KeySize
            );
            return Convert.ToBase64String(hash);
        }

        public async Task<LoginResponse> Login(LoginDto body)
        {
            var user = await this._userRepository.FindByEmailorPhone(body.Username);
            if (user == null) {
                throw new Exception("Sai user");
            }
            var cred = await this._userCredentialRepository.GetByWhereAsync(c=>c.Guid == user.Id);
            if(cred == null)
            {
                throw new Exception("Chưa co mat khau");
            }
            var target = cred.PasswordHash;
            var ver = this.HashPassword(cred.PasswordSalt, body.Password);
            if(!Equals(target, ver))
            {
                throw new Exception("sai mat khau");
            }
            return GenerateToken(user);

        }

        private LoginResponse GenerateToken(User user)
        {
            string issuer = _configuration.GetValue<string>("Issuer");
            string audience = _configuration.GetValue<string>("Audience");
            string secret = _configuration.GetValue<string>("Secret");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);
            return new LoginResponse
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
    }
}
