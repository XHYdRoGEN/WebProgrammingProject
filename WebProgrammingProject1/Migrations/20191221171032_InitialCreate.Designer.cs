﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProgrammingProject1.Context;

namespace WebProgrammingProject1.Migrations
{
    [DbContext(typeof(ShareItContext))]
    [Migration("20191221171032_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebProgrammingProject1.Models.Admins", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminUsername")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Categories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Comments", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Likes", b =>
                {
                    b.Property<int>("LikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PostID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("LikeID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Posts", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("PostCommentCount")
                        .HasColumnType("int");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("PostLikeCount")
                        .HasColumnType("int");

                    b.Property<string>("PostThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PostID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Comments", b =>
                {
                    b.HasOne("WebProgrammingProject1.Models.Posts", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID");

                    b.HasOne("WebProgrammingProject1.Models.Users", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Likes", b =>
                {
                    b.HasOne("WebProgrammingProject1.Models.Posts", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostID");

                    b.HasOne("WebProgrammingProject1.Models.Users", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("WebProgrammingProject1.Models.Posts", b =>
                {
                    b.HasOne("WebProgrammingProject1.Models.Categories", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryID");

                    b.HasOne("WebProgrammingProject1.Models.Users", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
