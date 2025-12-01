using Foundation.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Foundation.Business
{
    public class MongoDbContext : DbContext
    {
        public MongoDbContext(DbContextOptions<MongoDbContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>(p =>
            {
                p.ToCollection("messages");
                p.Property(x => x.ConversationId).HasElementName("conversation_id");
                p.HasIndex(x => x.ConversationId);
            });
        }
    }
}
