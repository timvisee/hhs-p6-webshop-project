using System;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Data {
    public static class DbBuilder {

        // TODO: Clean this class!

        /// <summary>
        /// Rebuild the selected database.
        ///
        /// Warning: This overrides the current data available in the database.
        /// </summary>
        /// <param name="context"></param>
        public static void Rebuild(ApplicationDbContext context) {
            // Show a status message to the user
            LogUtils.Warning("Starting database initialization...");

            Console.Write("Deleting database... (please be patient, this may take up to 20 seconds)", ConsoleColor.White);
            context.Database.EnsureDeleted();
            Console.Write("Provisioning new database... (please be patient, this may take up to 60 seconds)", ConsoleColor.White);
            context.Database.EnsureCreated();

            var user = new ApplicationUser {
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

            // Wait for the actual result
            result.Wait();

            LogUtils.Log("Generating products", ConsoleColor.White);

            GenerateProducts(context);

            // Show a success message
            LogUtils.Success("Database built!");

            // Save the changes to the database context
            context.SaveChanges();

            

            // Exit the application if configured, when the built process is complete
            if (Program.AppConfig.DatabaseExitAfterReset) {
                // Tell the user the application will exit
                LogUtils.Warning("The database has been built. The application will now exit.");

                // Actually exit the application
                Environment.Exit(0);
            }
        }

        private static PropertyTypeProduct Couple(Product p, PropertyType pt, object value) {
            return new PropertyTypeProduct(p, pt, new PropertyValue(value));
        }

        private static void GenerateProducts(ApplicationDbContext context) {
            PropertyType kleur = new PropertyType();
            kleur.DataType = typeof(string).FullName;
            kleur.Multiple = true;
            kleur.Name = "Kleur";
            kleur.Required = true;
            context.PropertyType.Add(kleur);

            PropertyType prijs = new PropertyType();
            prijs.DataType = typeof(double).FullName;
            prijs.Multiple = false;
            prijs.Name = "Prijs";
            prijs.Required = true;
            context.PropertyType.Add(prijs);

            context.SaveChanges();

            Product stoel = new Product();
            stoel.Name = "Stoel";
            stoel.Description = "Een hele grote stoel";
            context.Product.Add(stoel);

            Product bank = new Product();
            bank.Name = "Bank";
            bank.Description = "Een hele grote bank";
            context.Product.Add(bank);

            context.SaveChanges();

            context.PropertyTypeProducts.Add(Couple(stoel, prijs, 65.0));
            context.PropertyTypeProducts.Add(Couple(bank, prijs, 550.0));

            context.SaveChanges();

            context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Blauw"));
            context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Bruin"));
            context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Geel"));

            context.PropertyTypeProducts.Add(Couple(bank, kleur, "Grijs"));
            context.PropertyTypeProducts.Add(Couple(bank, kleur, "Wit"));
            context.PropertyTypeProducts.Add(Couple(bank, kleur, "Zwart"));

            context.SaveChanges();

        }
    }
}
