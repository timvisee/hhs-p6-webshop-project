using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Models.BlogModels;
using Microsoft.Extensions.Configuration;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext() : base(CreateTestContext()) { }

        static DbContextOptions<ApplicationDbContext> CreateTestContext() {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite("test.db");
            return builder.Options;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasMany(p => p.ColorOptions)
                .WithOne(co => co.Product)
                .HasForeignKey(co => co.ProductId);
            

            builder.Entity<ColorOption>()
                .HasMany(co => co.Images)
                .WithOne(pi => pi.ColorOption)
                .HasForeignKey(pi => pi.ColorOptionId);


            builder.Entity<BlogArticleCategory>().HasKey(x => new { x.BlogArticleId, x.BlogCategoryId });
        }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<AppointmentTime> AppointmentTime { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ColorOption> ColorOptions { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<BlogArticleCategory> BlogArticleCategory { get; set; }

        public DbSet<BlogArticle> BlogArticle { get; set; }

        public DbSet<BlogCategory> BlogCategory { get; set; }
    }
}