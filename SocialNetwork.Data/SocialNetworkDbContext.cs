﻿namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using SocialNetwork.Data.EntityDataModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UserSharedAlbums> UserSharedAlbums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyTempDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Friendship>()
                .HasKey(f => new { f.FromFriendId, f.ToFriendId });

            builder.Entity<User>()
                .HasMany(u => u.RelatedFrom)
                .WithOne(f => f.FromFriend)
                .HasForeignKey(f => f.FromFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.RelatedTo)
                .WithOne(f => f.ToFriend)
                .HasForeignKey(f => f.ToFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Album>()
                .HasOne(a => a.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(a => a.UserId)
                .HasConstraintName("FK_Albums_User_UserId");

            builder.Entity<AlbumPicture>()
                .HasKey(ap => new { ap.AlbumId, ap.PictureId });

            builder.Entity<AlbumPicture>(entity =>
            {
                entity
                    .HasOne(e => e.Album)
                    .WithMany(a => a.Pictures)
                    .HasForeignKey(e => e.AlbumId)
                    .HasConstraintName("FK_AlbumsPictures_Album_AlbumId");

                entity
                    .HasOne(e => e.Picture)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(e => e.PictureId)
                    .HasConstraintName("FK_AlbumsPictures_Picture_PictureId");
            });

            builder.Entity<AlbumTag>()
                .HasKey(at => new { at.AlbumId, at.TagId });

            builder.Entity<AlbumTag>(entity =>
            {
                entity
                    .HasOne(e => e.Album)
                    .WithMany(a => a.Tags)
                    .HasForeignKey(e => e.AlbumId)
                    .HasConstraintName("FK_AlbumTag_Album_AlbumId");

                entity
                    .HasOne(e => e.Tag)
                    .WithMany(t => t.Albums)
                    .HasForeignKey(e => e.TagId)
                    .HasConstraintName("FK_AlbumTag_Tag_tagId");
            });

            builder.Entity<UserSharedAlbums>()
                .HasKey(ua => new { ua.UserId, ua.SharedAlbumId });

            builder.Entity<UserSharedAlbums>(entity =>
            {
                entity
                    .HasOne(e => e.User)
                    .WithMany(u => u.SharedAlbums)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(e => e.SharedAlbum)
                    .WithMany(sa => sa.UsersWithRoles)
                    .HasForeignKey(e => e.SharedAlbumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}