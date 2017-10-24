﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SocialNetwork.Data;
using System;

namespace SocialNetwork.Data.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20171024035652_UsersSharedAlbums")]
    partial class UsersSharedAlbums
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BackgroundColor")
                        .IsRequired();

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.AlbumPicture", b =>
                {
                    b.Property<int>("AlbumId");

                    b.Property<int>("PictureId");

                    b.HasKey("AlbumId", "PictureId");

                    b.HasIndex("PictureId");

                    b.ToTable("AlbumPicture");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.AlbumTag", b =>
                {
                    b.Property<int>("AlbumId");

                    b.Property<int>("TagId");

                    b.HasKey("AlbumId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("AlbumTag");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Friendship", b =>
                {
                    b.Property<int>("FromFriendId");

                    b.Property<int>("ToFriendId");

                    b.HasKey("FromFriendId", "ToFriendId");

                    b.HasIndex("ToFriendId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagTitle")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Age");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastTymeLoggedIn");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("ProfilePicture")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("RegisteredOn");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.UserSharedAlbums", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("SharedAlbumId");

                    b.HasKey("UserId", "SharedAlbumId");

                    b.HasIndex("SharedAlbumId");

                    b.ToTable("UserSharedAlbums");
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Album", b =>
                {
                    b.HasOne("SocialNetwork.Data.EntityDataModels.User", "User")
                        .WithMany("Albums")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Albums_User_UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.AlbumPicture", b =>
                {
                    b.HasOne("SocialNetwork.Data.EntityDataModels.Album", "Album")
                        .WithMany("Pictures")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_AlbumsPictures_Album_AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialNetwork.Data.EntityDataModels.Picture", "Picture")
                        .WithMany("Albums")
                        .HasForeignKey("PictureId")
                        .HasConstraintName("FK_AlbumsPictures_Picture_PictureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.AlbumTag", b =>
                {
                    b.HasOne("SocialNetwork.Data.EntityDataModels.Album", "Album")
                        .WithMany("Tags")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_AlbumTag_Album_AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialNetwork.Data.EntityDataModels.Tag", "Tag")
                        .WithMany("Albums")
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_AlbumTag_Tag_tagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.Friendship", b =>
                {
                    b.HasOne("SocialNetwork.Data.EntityDataModels.User", "FromFriend")
                        .WithMany("RelatedFrom")
                        .HasForeignKey("FromFriendId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SocialNetwork.Data.EntityDataModels.User", "ToFriend")
                        .WithMany("RelatedTo")
                        .HasForeignKey("ToFriendId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SocialNetwork.Data.EntityDataModels.UserSharedAlbums", b =>
                {
                    b.HasOne("SocialNetwork.Data.EntityDataModels.Album", "SharedAlbum")
                        .WithMany("UsersWithRoles")
                        .HasForeignKey("SharedAlbumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SocialNetwork.Data.EntityDataModels.User", "User")
                        .WithMany("SharedAlbums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
