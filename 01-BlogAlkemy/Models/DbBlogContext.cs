using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_BlogAlkemy.Models
{
    public class DbBlogContext: DbContext
    {
        public DbBlogContext()
        {

        }
        public DbBlogContext(DbContextOptions<DbBlogContext> options) : base(options)
        {

        }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");
                entity.HasKey(e => e.IdCategory);
                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Post>(entity => 
            {
                entity.ToTable("Post");
                entity.HasKey(e => e.IdPost);
                entity.Property(e => e.IdPost).HasColumnName("idPost");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Posts)
                    .HasForeignKey(p => p.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_category");

            });
        }
    }
}
