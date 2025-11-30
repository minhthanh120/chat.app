using System.ComponentModel.DataAnnotations;

namespace chat.api.Dtos
{
    public class CreateUserDto
    {
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(10)]
        public string? Phone { get; set; }
        public string Password { get; set; }
    }
}
