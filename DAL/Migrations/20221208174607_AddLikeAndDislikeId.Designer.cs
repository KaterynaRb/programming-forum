﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221208174607_AddLikeAndDislikeId")]
    partial class AddLikeAndDislikeId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Dislike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PostReplyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("PostReplyId");

                    b.HasIndex("UserId");

                    b.ToTable("Dislikes");
                });

            modelBuilder.Entity("DAL.Entities.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PostReplyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("PostReplyId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("DAL.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AcceptedReplyId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DAL.Entities.PostReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int?>("ParentReplyId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ParentReplyId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostReplies");
                });

            modelBuilder.Entity("DAL.Entities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Chapter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Entities.Dislike", b =>
                {
                    b.HasOne("DAL.Entities.Post", "Post")
                        .WithMany("Dislikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.PostReply", "PostReply")
                        .WithMany("Dislikes")
                        .HasForeignKey("PostReplyId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("DAL.Entities.User", "User")
                        .WithMany("Dislikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("PostReply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Like", b =>
                {
                    b.HasOne("DAL.Entities.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.PostReply", "PostReply")
                        .WithMany("Likes")
                        .HasForeignKey("PostReplyId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("DAL.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("PostReply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Post", b =>
                {
                    b.HasOne("DAL.Entities.Topic", "Topic")
                        .WithMany("Posts")
                        .HasForeignKey("TopicId");

                    b.HasOne("DAL.Entities.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("DAL.Entities.PostReply", b =>
                {
                    b.HasOne("DAL.Entities.PostReply", "ParentReply")
                        .WithMany("Replies")
                        .HasForeignKey("ParentReplyId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("DAL.Entities.Post", "Post")
                        .WithMany("PostReplies")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.User", "Author")
                        .WithMany("PostReplies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ParentReply");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DAL.Entities.Post", b =>
                {
                    b.Navigation("Dislikes");

                    b.Navigation("Likes");

                    b.Navigation("PostReplies");
                });

            modelBuilder.Entity("DAL.Entities.PostReply", b =>
                {
                    b.Navigation("Dislikes");

                    b.Navigation("Likes");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("DAL.Entities.Topic", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Navigation("Dislikes");

                    b.Navigation("Likes");

                    b.Navigation("PostReplies");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
