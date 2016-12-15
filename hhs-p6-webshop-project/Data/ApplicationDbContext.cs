using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.AppointmentModels;

namespace hhs_p6_webshop_project.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public static bool reset = false;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {

            if (reset) {
#if DEBUG
                //Reset everything, beun maar werkt
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Resetting database! ALL DATA WILL BE LOST!");
                Console.ForegroundColor = ConsoleColor.White;
                Database.EnsureDeleted();
                Database.EnsureCreated();
#else
                Console.WriteLine("Not resetting database, system is running in production mode!");
#endif
                reset = false;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<AppointmentTime> AppointmentTime { get; set; }
    }
}