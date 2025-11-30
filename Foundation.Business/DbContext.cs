using Microsoft.EntityFrameworkCore;
using Foundation.Models.Entities;
using Foundation.Models;
using Microsoft.Extensions.Configuration;
namespace Foundation.Business
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");
            modelBuilder.Entity("Foundation.Models.Conversation");
        }
    }
}
