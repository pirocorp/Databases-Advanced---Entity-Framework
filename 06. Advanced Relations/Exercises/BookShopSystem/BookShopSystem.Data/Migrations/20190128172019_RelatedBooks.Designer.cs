﻿// <auto-generated />
using BookShopSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookShopSystem.Data.Migrations
{
    [DbContext(typeof(BookShopSystemContext))]
    [Migration("20190128172019_RelatedBooks")]
    partial class RelatedBooks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookShopSystem.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookShopSystem.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<int>("Copies");

                    b.Property<string>("Description");

                    b.Property<int>("Edition");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookShopSystem.Models.BookRelatedBook", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("RelatedId");

                    b.HasKey("BookId", "RelatedId");

                    b.HasIndex("RelatedId");

                    b.ToTable("RelatedBooks");
                });

            modelBuilder.Entity("BookShopSystem.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookShopSystem.Models.CategoryBook", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("CategoryId");

                    b.HasKey("BookId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoriesBooks");
                });

            modelBuilder.Entity("BookShopSystem.Models.Book", b =>
                {
                    b.HasOne("BookShopSystem.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShopSystem.Models.BookRelatedBook", b =>
                {
                    b.HasOne("BookShopSystem.Models.Book", "Book")
                        .WithMany("RelatedBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShopSystem.Models.Book", "RelatedBook")
                        .WithMany()
                        .HasForeignKey("RelatedId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BookShopSystem.Models.CategoryBook", b =>
                {
                    b.HasOne("BookShopSystem.Models.Book", "Book")
                        .WithMany("Categories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShopSystem.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
