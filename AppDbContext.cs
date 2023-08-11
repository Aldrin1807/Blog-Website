using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            //Many to Many for Likes
           modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserID, l.BlogID });
            modelBuilder.Entity<Like>()
                .HasOne(u=>u.User)
                .WithMany(b=>b.Likes)
                .HasForeignKey(u=>u.UserID)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Like>()
                .HasOne(b => b.Blog)
                .WithMany(u => u.Likes)
                .HasForeignKey(u => u.BlogID)
                .OnDelete(DeleteBehavior.NoAction);

            //Many to many for Comments
            modelBuilder.Entity<Comment>()
                .HasKey(c => new {c.Id,c.BlogPostId,c.UserId });
            modelBuilder.Entity<Comment>()
                .HasOne(u=> u.User)
                .WithMany(c=>c.Comments)
                .HasForeignKey(u=>u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne(u=>u.Blog)
                .WithMany(c=>c.Comments)
                .HasForeignKey(u=>u.BlogPostId)
                .OnDelete(DeleteBehavior.NoAction);


            

        }


        public DbSet<Models.Blog> Blogs { get; set; }   
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
