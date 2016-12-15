using System;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Data
{
    public static class DbInitializer {
        public static bool Run { get; set; }

        public static void Initialize(ApplicationDbContext context) {
            if (!Run) {
                LogUtils.Log("-----------------------------------------------------", ConsoleColor.Green);
                LogUtils.Log("| Not running database initializer...               |", ConsoleColor.Green);
                LogUtils.Log("| DATA WILL BE PRESERVED                            |", ConsoleColor.Green);
                LogUtils.Log("-----------------------------------------------------", ConsoleColor.Green);
                return;
            }

            LogUtils.Log("-----------------------------------------------------");
            LogUtils.Log("| Starting database initialization...               |");
            LogUtils.Log("| ALL DATA WILL BE LOST!                            |");
            LogUtils.Log("-----------------------------------------------------");


            LogUtils.Log("Deleting database... (please be patient, this may take up to 20 seconds)", ConsoleColor.White);
            context.Database.EnsureDeleted();
            LogUtils.Log("Provisioning new database... (please be patient, this may take up to 60 seconds)", ConsoleColor.White);
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

            LogUtils.Log("Seed completed!", ConsoleColor.Green);
            
            context.SaveChanges();
        }
    }
}
