using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foundation.Models
{
    [Table("conversation")]
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Guid? LatestMessageId { get; set; }
        public DateTime? LatestMessageAt { get; set; }

    }
}
