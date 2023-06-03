using Microsoft.EntityFrameworkCore;

namespace task6x.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<MessageModel> Messages { get; set; } = null!;

        public ChatContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageModel>().HasKey(u => u.ID);
            modelBuilder.Entity<UserModel>().HasKey(u => u.UserName);
        }

        //public ChatContext(DbContextOptions<ChatContext> options)
        public ChatContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}