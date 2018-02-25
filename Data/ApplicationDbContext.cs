using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForumApp.Models;

namespace ForumApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

            public DbSet<Posting> Posting { get; set; }
            public DbSet<Topic> Topic { get; set; }
            public DbSet<Category> Category { get; set; }
            public DbSet<Group> Group { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Tables
            builder.Entity<Posting>().ToTable("posting");
            builder.Entity<Topic>().ToTable("topic");
            builder.Entity<Category>().ToTable("category");
            builder.Entity<Group>().ToTable("group");
            
            //Keys
            builder.Entity<Posting>().HasKey(p => new {p.idPosting, p.idTopic, p.idUser});
            builder.Entity<Topic>().HasKey(t => new {t.idTopic, t.idCategory, t.idUser});
            builder.Entity<Category>().HasKey(c => new {c.idCategory, c.idGroup});
            builder.Entity<Group>().HasKey(g => g.idGroup);
        }
    }
    class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-0LUEK7F;Database=forumapp;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=eduardo0123");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            return dbContext;
        }
    }
}
