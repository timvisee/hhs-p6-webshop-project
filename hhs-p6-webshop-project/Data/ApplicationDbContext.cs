using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.AppointmentModels;
using Microsoft.Extensions.Configuration;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext() : base(CreateTestContext()) {}

        static DbContextOptions<ApplicationDbContext> CreateTestContext() {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite("test.db");
            return builder.Options;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<PropertyTypeProduct>()
                .HasKey(x => new {x.ProductId, x.PropertyTypeId, x.PropertyValueId});

            builder.Entity<PropertyTypeProduct>()
                .HasOne(ptp => ptp.PropertyValue)
                .WithOne(pv => pv.PropertyTypeProduct);

            builder.Entity<PropertyTypeProduct>()
                .HasOne(ptp => ptp.Product)
                .WithMany(p => p.PropertyTypeProducts)
                .HasForeignKey(fk => fk.ProductId);
        }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<AppointmentTime> AppointmentTime { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<PropertyValue> PropertyValue { get; set; }

        public DbSet<PropertyType> PropertyType { get; set; }

        public DbSet<PropertyTypeProduct> PropertyTypeProducts { get; set; }

        //public DbSet<ProductImage> ProductImage { get; set; }


      
    }
}