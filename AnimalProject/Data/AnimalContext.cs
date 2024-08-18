using AnimalProject.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace AnimalProject.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between Animal and Comment (One-to-Many)
            modelBuilder.Entity<Comment>()
                .HasOne(a => a.Animal)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.AnimalId);

            // Seed data
            modelBuilder.Entity<Animal>().HasData(
                new { AnimalId = 123, Name = "Dog", Age = 8, PictureName = "pictureDog2.jpg", Description = "Walks on 4 legs", CategoryId = 1 },
                new { AnimalId = 234, Name = "Cat", Age = 7, PictureName = "pictureCat1.jpg", Description = "Walks on 4 legs", CategoryId = 1 },
                new { AnimalId = 345, Name = "Parrot", Age = 6, PictureName = "pictureParrot1.jpg", Description = "Knows how to fly", CategoryId = 2 },
                new { AnimalId = 456, Name = "GoldFish", Age = 1, PictureName = "pictureGoldFish1.jpg", Description = "Knows how to swim", CategoryId = 4 }
            );
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 001, Name = "Mammals" }, 
                new { CategoryId = 002, Name = "Birds" },
                new { CategoryId = 004, Name = "Fish" }
                );
            modelBuilder.Entity<Comment>().HasData(
                new { CommentId = 001, AnimalId = 123, comment= "Its color is white" },
                new { CommentId = 002, AnimalId = 234, comment = "Its color is reddish" },
                new { CommentId = 003, AnimalId = 456, comment = "He swims the fastest" },
                new { CommentId = 004, AnimalId = 234, comment = "likes to play very much" },
                new { CommentId = 005, AnimalId = 456, comment = "likes to play very much" },
                new { CommentId = 006, AnimalId = 456, comment = "likes to play very much" }
                );

        }
    }
}

