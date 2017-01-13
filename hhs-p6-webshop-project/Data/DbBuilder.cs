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

            #region dress

            Product dress = new Product();
            dress.Name = "Ladybird";
            dress.Description = "Een ladybird jurk";

            ProductType dress_klein = new ProductType();
            dress_klein.NameOverride = "Kleine Jurk";
            dress_klein.DescriptionOverride = "Een kleine jurk";
            dress_klein.Images.Add(new ProductImage("images//uploads/image-1.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress_klein, prijs, 50.0d));
            context.PropertyValueCouplings.Add(Couple(dress_klein, kleur, "Wit"));
            context.PropertyValueCouplings.Add(Couple(dress_klein, kleur, "Bruin"));

            dress.ProductTypes.Add(dress_klein);

            context.Product.Add(dress);
            context.SaveChanges();

            #endregion

            #region dresss 

            Product dresss = new Product();
            dresss.Name = "Ladybird";
            dresss.Description = "Een ladybird jurk";

            ProductType jurk_bruin = new ProductType();
            jurk_bruin.NameOverride = "Bruine jurk";
            jurk_bruin.DescriptionOverride = "Een bruine ladybird jurk";
            jurk_bruin.Images.Add(new ProductImage("images/ladybirddress3.jpg"));

            context.PropertyValueCouplings.Add(Couple(jurk_bruin, prijs, 75.0d));
            context.PropertyValueCouplings.Add(Couple(jurk_bruin, kleur, "Grijs"));
            context.PropertyValueCouplings.Add(Couple(jurk_bruin, kleur, "Geel"));

            dresss.ProductTypes.Add(jurk_bruin);

            context.Product.Add(dresss);
            context.SaveChanges();

            #endregion

            #region jurk 

            Product jurk = new Product();
            jurk.Name = "Manbird";
            jurk.Description = "Nog een jurk";

            ProductType jurkType2 = new ProductType();
            jurkType2.NameOverride = "Prachtige jurk";
            jurkType2.DescriptionOverride = "Een mooie jurk";
            jurkType2.Images.Add(new ProductImage("images/uploads/image-3.jpg"));

            context.PropertyValueCouplings.Add(Couple(jurkType2, prijs, 500.0d));
            context.PropertyValueCouplings.Add(Couple(jurkType2, kleur, "Wit kant"));
            context.PropertyValueCouplings.Add(Couple(jurkType2, kleur, "Bruin Leer"));

            jurk.ProductTypes.Add(jurkType2);

            context.Product.Add(jurk);
            context.SaveChanges();
            #endregion

            #region jurkk

            Product jurkk = new Product();
            jurkk.Name = "Manbird";
            jurkk.Description = "Nog een jurk";

            ProductType jurkType3 = new ProductType();
            jurkType3.NameOverride = "Prachtige jurk, geslaagde bruiloft";
            jurkType3.DescriptionOverride = "Met deze jurk komt alles goed";
            jurkType3.Images.Add(new ProductImage("images/uploads/image-5.jpg"));

            context.PropertyValueCouplings.Add(Couple(jurkType3, prijs, 750.0d));
            context.PropertyValueCouplings.Add(Couple(jurkType3, kleur, "Grijs Leer"));
            context.PropertyValueCouplings.Add(Couple(jurkType3, kleur, "Zwart Leer"));

            jurkk.ProductTypes.Add(jurkType3);

            context.Product.Add(jurkk);
            context.SaveChanges();

            #endregion

            #region jurk1

            Product jurk1 = new Product();
            jurk1.Name = "Ladybird 1";
            jurk1.Description = "Een ladybird jurk";

            ProductType dress1 = new ProductType();
            dress1.NameOverride = "Kleine Jurk";
            dress1.DescriptionOverride = "Een kleine jurk";
            dress1.Images.Add(new ProductImage("images/uploads/image-1.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress1, prijs, 50.0d));
            context.PropertyValueCouplings.Add(Couple(dress1, kleur, "Wit"));
            context.PropertyValueCouplings.Add(Couple(dress1, kleur, "Bruin"));

            jurk1.ProductTypes.Add(dress1);
            context.Product.Add(jurk1);
            #endregion

            #region jurk2

            Product jurk2 = new Product();
            jurk2.Name = "Ladybird 2";
            jurk2.Description = "Een ladybird jurk";

            ProductType dress2 = new ProductType();
            dress2.NameOverride = "Bruine jurk";
            dress2.DescriptionOverride = "Een bruine ladybird jurk";
            dress2.Images.Add(new ProductImage("images/uploads/image-2.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress2, prijs, 65.0d));
            context.PropertyValueCouplings.Add(Couple(dress2, kleur, "Grijs"));
            context.PropertyValueCouplings.Add(Couple(dress2, kleur, "Geel"));

            jurk2.ProductTypes.Add(dress2);

            context.Product.Add(jurk2);

            #endregion

            #region jurk3

            Product jurk3 = new Product();
            jurk3.Name = "Ladybird 3";
            jurk3.Description = "Een ladybird jurk";

            ProductType dress3 = new ProductType();
            dress3.NameOverride = "Witte jurk";
            dress3.DescriptionOverride = "Een witte ladybird jurk";
            dress3.Images.Add(new ProductImage("images/uploads/image-3.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress3, prijs, 95.0d));
            context.PropertyValueCouplings.Add(Couple(dress3, kleur, "Grijs"));
            context.PropertyValueCouplings.Add(Couple(dress3, kleur, "Geel"));

            jurk3.ProductTypes.Add(dress3);

            context.Product.Add(jurk3);

            #endregion

            #region jurk4

            Product jurk4 = new Product();
            jurk4.Name = "Ladybird 4";
            jurk4.Description = "Een ladybird jurk";

            ProductType dress4 = new ProductType();
            dress4.NameOverride = "Blauwe jurk";
            dress4.DescriptionOverride = "Een blauwe ladybird jurk";
            dress4.Images.Add(new ProductImage("images/uploads/image-4.jpg"));

            context.PropertyValueCouplings.Add(Couple(dress4, prijs, 80.0d));
            context.PropertyValueCouplings.Add(Couple(dress4, kleur, "Grijs"));
            context.PropertyValueCouplings.Add(Couple(dress4, kleur, "Geel"));

            jurk4.ProductTypes.Add(dress4);

            context.Product.Add(jurk4);
            context.SaveChanges();

            #endregion
            
        }
    }
}
