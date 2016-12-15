using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Data
{
    public static class DbInitializer {
        public static bool Run { get; set; }

        public static void Initialize(ApplicationDbContext context) {
            if (!Run) {
                Program.Log("-----------------------------------------------------", ConsoleColor.Green);
                Program.Log("| Not running database initializer...               |", ConsoleColor.Green);
                Program.Log("| DATA WILL BE PRESERVED                            |", ConsoleColor.Green);
                Program.Log("-----------------------------------------------------", ConsoleColor.Green);
                return;
            }

            Program.Log("-----------------------------------------------------");
            Program.Log("| Starting database initialization...               |");
            Program.Log("| ALL DATA WILL BE LOST!                            |");
            Program.Log("-----------------------------------------------------");


            Program.Log("Deleting database... (please be patient, this may take up to 20 seconds)", ConsoleColor.White);
            context.Database.EnsureDeleted();
            Program.Log("Provisioning new database... (please be patient, this may take up to 60 seconds)", ConsoleColor.White);
            context.Database.EnsureCreated();

            var user = new ApplicationUser
            {
                Email = "beun@beun.it",
                NormalizedEmail = "BEUN@BEUN.IT",
                UserName = "beun@beun.it",
                NormalizedUserName = "BEUN@BEUN.IT",
                PhoneNumber = "0906-jemoeder",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "beun");
            user.PasswordHash = hashed;

            var userStore = new UserStore<ApplicationUser>(context);
            var result = userStore.CreateAsync(user);

            result.Wait();

            Program.Log("Seed completed!", ConsoleColor.Green);
            
            context.SaveChanges();
        }
    }
}
