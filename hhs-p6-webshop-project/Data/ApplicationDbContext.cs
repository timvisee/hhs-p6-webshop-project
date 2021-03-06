﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Models.NewsModels;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base(CreateTestContext())
        {
        }

        static DbContextOptions<ApplicationDbContext> CreateTestContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite("Data source=test.db");
            return builder.Options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasMany(p => p.ColorOptions)
                .WithOne(co => co.Product)
                .HasForeignKey(co => co.ProductId);


            builder.Entity<ColorOption>()
                .HasMany(co => co.Images)
                .WithOne(pi => pi.ColorOption)
                .HasForeignKey(pi => pi.ColorOptionId);


            builder.Entity<NewsArticleCategory>()
                .HasKey(sc => new {sc.NewsArticleID, sc.NewsCategoryID});

            builder.Entity<NewsArticleCategory>()
                .HasOne(sc => sc.NewsArticle)
                .WithMany(s => s.NewsArticleCategories)
                .HasForeignKey(sc => sc.NewsArticleID);

            builder.Entity<NewsArticleCategory>()
                .HasOne(sc => sc.NewsCategory)
                .WithMany(c => c.NewsArticleCategories)
                .HasForeignKey(sc => sc.NewsCategoryID);
        }

        public virtual DbSet<Appointment> Appointment { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ColorOption> ColorOptions { get; set; }

        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<NewsArticle> NewsArticle { get; set; }

        public virtual DbSet<NewsCategory> NewsCategory { get; set; }

        public virtual DbSet<NewsArticleCategory> NewsArticleCategory { get; set; }
    }
}