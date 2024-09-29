using Itrantion.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Itrantion.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<PermissionModel> Permissions { get; set; } = null!;
        public DbSet<PresentationModel> Presentations { get; set; } = null!;
        public DbSet<SlideModel> Slides { get; set; } = null!;
        public DbSet<TextModel> Texts { get; set; } = null!;
        public DbSet<UserConnection> Connections { get; set; } = null!;

        public DatabaseContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserConnection>()
                .HasKey(uc => new { uc.User, uc.Presentation });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=MYSQL8002.site4now.net;Database=db_aad39d_store1;Uid=aad39d_store1;Pwd=1234567A",
                new MySqlServerVersion(new Version(8, 3, 0)),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure());
        }
    }
}