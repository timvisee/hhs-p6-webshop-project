using System;
using System.Collections.Generic;
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

        private static Product GenerateProduct(string name, string description, double price) {
            return new Product() {
                Name = name,
                Description = description,
                Price = price
            };
        }

        private static ColorOption GenerateColor(string color, string[] images) {
            var co = new ColorOption() {
                Color = color
            };

            foreach(string img in images)
            {
                co.Images.Add(new ProductImage(img));
            }

            return co;
        }

        
        private static void GenerateProducts(ApplicationDbContext context) {
            Product p = GenerateProduct("Orea Sposa",
                "Trouwjurk van het merk Orea Sposa gemaakt van kant. De top is strapless met een sweetheart lijn. De taille wordt geaccentueerd door een bies. De rok heeft een A-lijn met een sleep.",
                500.0d);

            p.ColorOptions.Add(GenerateColor("Gekleurd", new string[] {"images/dress/orea_sposa/roze/1.jpg","images/dress/orea_sposa/roze/2.jpg","images/dress/orea_sposa/roze/3.jpg"}));
            p.ColorOptions.Add(GenerateColor("Ivoor/Wit", new string[] {"images/dress/orea_sposa/grijs/1.jpg","images/dress/orea_sposa/grijs/2.jpg","images/dress/orea_sposa/grijs/3.jpg"}));

            context.Products.Add(p);
            context.SaveChanges();


            p = GenerateProduct("Ladybird",
                "Trouwjurk van het merk Ladybird gemaakt van kant. De top is strapless met een sweetheart lijn. De rok heeft een A-lijn met een sleep.",
                1500.0d);

            p.ColorOptions.Add(GenerateColor("Ivoor/Wit", new string[] {"images/dress/ladybird/ivoor/1.jpg","images/dress/ladybird/ivoor/2.jpg","images/dress/ladybird/ivoor/3.jpg"}));
            p.ColorOptions.Add(GenerateColor("Ivoor met kleur", new string[] {"images/dress/ladybird/grijs/1.jpg","images/dress/ladybird/grijs/2.jpg","images/dress/ladybird/grijs/3.jpg"}));

            context.Products.Add(p);
            context.SaveChanges();

            //Product p = GenerateProduct("Orea Sposa",
            //    "Trouwjurk van het merk Orea Sposa gemaakt van kant. De top is strapless met een sweetheart lijn. De taille wordt geaccentueerd door een bies. De rok heeft een A-lijn met een sleep.",
            //    500.0d);

            //p.ColorOptions.Add(GenerateColor("Roze", new string[] {"images/dress/orea_sposa/roze/1.jpg","images/dress/roze/2.jpg","images/dress/roze/3.jpg"}));
            //p.ColorOptions.Add(GenerateColor("Grijs", new string[] {"images/dress/orea_sposa/grijs/1.jpg","images/dress/grijs/2.jpg","images/dress/grijs/3.jpg"}));


            //PropertyType kleur = new PropertyType();
            //kleur.DataType = typeof(string).FullName;
            //kleur.Multiple = true;
            //kleur.Name = "Kleur";
            //kleur.Required = true;
            //context.PropertyType.Add(kleur);

            //PropertyType prijs = new PropertyType();
            //prijs.DataType = typeof(double).FullName;
            //prijs.Multiple = false;
            //prijs.Name = "Prijs";
            //prijs.Required = true;
            //context.PropertyType.Add(prijs);

            //context.SaveChanges();

            //Product stoel = new Product();
            //stoel.Name = "Stoel";
            //stoel.Description = "Een hele grote stoel";
            //context.Product.Add(stoel);

            //Product bank = new Product();
            //bank.Name = "Bank";
            //bank.Description = "Een hele grote bank";
            //context.Product.Add(bank);

            //context.SaveChanges();

            //context.PropertyTypeProducts.Add(Couple(stoel, prijs, 65.0));
            //context.PropertyTypeProducts.Add(Couple(bank, prijs, 550.0));

            //context.SaveChanges();

            //context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Blauw"));
            //context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Bruin"));
            //context.PropertyTypeProducts.Add(Couple(stoel, kleur, "Geel"));

            //context.PropertyTypeProducts.Add(Couple(bank, kleur, "Grijs"));
            //context.PropertyTypeProducts.Add(Couple(bank, kleur, "Wit"));
            //context.PropertyTypeProducts.Add(Couple(bank, kleur, "Zwart"));

            //context.SaveChanges();

        }
    }
}
