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

        private static PropertyValueCoupling Couple(ProductType p, PropertyType pt, object value) {
            return new PropertyValueCoupling(p, pt, new PropertyValue(value));
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

            #region Dress

            Product dress = new Product();
            dress.Name = "Ladybird";
            dress.Description = "Een ladybird jurk";

            ProductType dress_klein = new ProductType();
            dress_klein.NameOverride = "Kleine Jurk";
            dress_klein.DescriptionOverride = "Een kleine jurk";
            dress_klein.Images.Add(new ProductImage("images/ladybirddress.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress_klein, prijs, 50.0d));
            context.PropertyValueCouplings.Add(Couple(dress_klein, kleur, "Wit"));
            context.PropertyValueCouplings.Add(Couple(dress_klein, kleur, "Bruin"));

            dress.ProductTypes.Add(dress_klein);

            ProductType jurk_bruin = new ProductType();
            jurk_bruin.NameOverride = "Bruine jurk";
            jurk_bruin.DescriptionOverride = "Een bruine ladybird jurk";
            jurk_bruin.Images.Add(new ProductImage("images/ladybirddress3.jpg"));

            context.PropertyValueCouplings.Add(Couple(jurk_bruin, prijs, 75.0d));
            context.PropertyValueCouplings.Add(Couple(jurk_bruin, kleur, "Grijs"));
            context.PropertyValueCouplings.Add(Couple(jurk_bruin, kleur, "Geel"));

            dress.ProductTypes.Add(jurk_bruin);

            context.Product.Add(dress);
            context.SaveChanges();

            #endregion

            #region Bank

            Product bank = new Product();
            bank.Name = "Bank";
            bank.Description = "Een bank";

            ProductType bank_klein = new ProductType();
            bank_klein.NameOverride = "2-persoons bank";
            bank_klein.DescriptionOverride = "Een kleine bank voor twee personen";
            bank_klein.Images.Add(new ProductImage("images/bank/klein.png"));

            context.PropertyValueCouplings.Add(Couple(bank_klein, prijs, 500.0d));
            context.PropertyValueCouplings.Add(Couple(bank_klein, kleur, "Wit Leer"));
            context.PropertyValueCouplings.Add(Couple(bank_klein, kleur, "Bruin Leer"));

            bank.ProductTypes.Add(bank_klein);

            ProductType bank_groot = new ProductType();
            bank_groot.NameOverride = "4-persoons bank";
            bank_groot.DescriptionOverride = "Een grote bank voor vier personen";
            bank_groot.Images.Add(new ProductImage("images/bank/groot.png"));

            context.PropertyValueCouplings.Add(Couple(bank_groot, prijs, 750.0d));
            context.PropertyValueCouplings.Add(Couple(bank_groot, kleur, "Grijs Leer"));
            context.PropertyValueCouplings.Add(Couple(bank_groot, kleur, "Zwart Leer"));

            bank.ProductTypes.Add(bank_groot);

            context.Product.Add(bank);
            context.SaveChanges();

            #endregion
            
        }
    }
}
