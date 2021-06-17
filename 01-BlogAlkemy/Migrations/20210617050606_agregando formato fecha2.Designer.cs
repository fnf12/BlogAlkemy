﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _01_BlogAlkemy.Models;

namespace _01_BlogAlkemy.Migrations
{
    [DbContext(typeof(DbBlogContext))]
    [Migration("20210617050606_agregando formato fecha2")]
    partial class agregandoformatofecha2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_01_BlogAlkemy.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idCategory")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("IdCategory");

                    b.ToTable("category");
                });

            modelBuilder.Entity("_01_BlogAlkemy.Models.Post", b =>
                {
                    b.Property<int>("IdPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPost")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8000)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("creationDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("idCategory");

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("image");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("IdPost");

                    b.HasIndex("IdCategory");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("_01_BlogAlkemy.Models.Post", b =>
                {
                    b.HasOne("_01_BlogAlkemy.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("FK_post_category")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("_01_BlogAlkemy.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
