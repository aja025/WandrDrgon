 using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WanderDragon.Models;

namespace WanderDragon.Data
{
    public partial class WanderDragonContext : DbContext
    {
        public virtual DbSet<DragonSprite> DragonSprites {get;set;}
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Dragon> Dragons { get; set; }
        public virtual DbSet<TravelLog> TravelLogs { get; set; }
        public virtual DbSet<ItemSprite> ItemSprites { get; set; }
        public virtual DbSet<ItemLog> ItemLogs { get; set; }

        
        // Example code to add database migration for new class:
        // dotnet ef migrations add [ filename ] --context wanderdragoncontext -o Data\Migrations
        // dotnet ef database update [ filename ] --context wanderdragoncontext

        public WanderDragonContext(DbContextOptions<WanderDragonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DragonSprite>().ToTable("DragonSprite");
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<Dragon>().ToTable("Dragon");
            modelBuilder.Entity<ItemSprite>().ToTable("ItemSprite");
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(t => t.UserId)
                    .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ItemLog>().ToTable("ItemLog");
            modelBuilder.Entity<TravelLog>().ToTable("TravelLog");

            // modelBuilder.Entity<DragonSprite>(entity =>
            // {
            //         entity.Property(t => t.DragonSpriteId);
            // });

            // Examples of Lambda expressions ****
            //
            // modelBuilder.Entity<Album>(entity =>
            // {
            //     entity.Property(e => e.Title)
            //         .IsRequired()
            //         .HasMaxLength(160);

            //     entity.HasOne(d => d.Artist)
            //         .WithMany(p => p.Album)
            //         .HasForeignKey(d => d.ArtistId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("FK_Album_Artist");
            // });
        }
    }
}