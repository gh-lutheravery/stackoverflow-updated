using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class StackOverflowCloneContext : DbContext
    {
        public StackOverflowCloneContext(DbContextOptions<StackOverflowCloneContext> options) : base(options)
        {
        }

        public DbSet<Question> Question => Set<Question>();
        public DbSet<Answer> Answer => Set<Answer>();
        public DbSet<Tag> Tag => Set<Tag>();
        public DbSet<Profile> Profile => Set<Profile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // creates QuestionTag table to map many-to-many relation between tags and questions
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Questions);
        }
    }
}
