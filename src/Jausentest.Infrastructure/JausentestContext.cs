using Jausentest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jausentest.Infrastructure
{
    public class JausentestContext : IdentityDbContext<UserEntity, UserRoleEntity, string>
    {
        public DbSet<BeislEntity> Beisl { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<BeislEntity>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Beisl)
                .UsingEntity(x => x.ToTable("BeislTags"))
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<BeislEntity>()
                .HasMany(b => b.Ratings)
                .WithOne(r => r.Beisl);
            
            modelBuilder
                .Entity<TagEntity>()
                .HasKey(t => t.Name);

            modelBuilder
                .Entity<RatingEntity>()
                .HasKey(r => r.Id);

            modelBuilder
                .Entity<ImageEntity>()
                .HasKey(i => i.Id);

            //Database seed for testing
            modelBuilder.Seed();

            
        }

        public JausentestContext(DbContextOptions<JausentestContext> options) : base(options)
        {

        }


    }
}
