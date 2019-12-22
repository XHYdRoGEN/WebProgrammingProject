using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1.Context
{
    public class ShareItContext:DbContext
    {
        public ShareItContext(DbContextOptions<ShareItContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(c => c.Comments);

            modelBuilder.Entity<Posts>()
                .HasOne(c => c.User)
                .WithMany(c => c.Posts);

            modelBuilder.Entity<Likes>()
                .HasOne(c => c.User)
                .WithMany(c => c.Likes);

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    UserID = 1,
                    Comments = null,
                    isAdmin = true,
                    Likes = null,
                    Posts = null,
                    UserEmail = "admin@admin.com",
                    UserName = "Admin",
                    UserNickName = "admin",
                    UserPassword = "123456",
                    UserSurname = "Admin"
                }
                );
        }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Users> Users { get; set; }

       
    }
}
