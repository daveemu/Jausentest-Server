using Jausentest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jausentest.Infrastructure;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BeislEntity>().HasData(
            new BeislEntity
            {
                Id = 1,
                Name = "Gacknpichler Mostschank",
                Owner = "Sepp Gacknpichler"
            });
        
        modelBuilder.Entity<BeislEntity>()
            .OwnsOne(a => a.Address)
            .HasData(
                new 
                {
                    BeislEntityId = 1L,
                    Street = "Hintaschling 7",
                    City = "Oberbrunzmosham",
                    ZipCode = "0815",
                    Lat = "48.206581",
                    Long = "13.814180"
                });

        modelBuilder.Entity<TagEntity>().HasData(
            new TagEntity
            {
                Name = "Most"
            });

        modelBuilder
            .Entity<BeislEntity>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.Beisl)
            .UsingEntity(x => x.ToTable("BeislTags").HasData(
                new { BeislId = 1L, TagsName = "Most" }));

        modelBuilder.Entity<RatingEntity>()
            .HasData(
                new
                {
                    BeislId = 1L,
                    Id =  1L,
                    Comment = "Muats an Saurausch hauma ghot!",
                    Score = 5.0
                });


    }
}