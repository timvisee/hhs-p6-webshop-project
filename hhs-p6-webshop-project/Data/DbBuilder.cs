﻿using System;
using System.Collections.Generic;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Data
{
    public static class DbBuilder
    {

        // TODO: Clean this class!

        /// <summary>
        /// Rebuild the selected database.
        ///
        /// Warning: This overrides the current data available in the database.
        /// </summary>
        /// <param name="context"></param>
        public static void Rebuild(ApplicationDbContext context)
        {
            // Show a status message to the user
            LogUtils.Warning("Starting database initialization...");

            Console.Write("Deleting database... (please be patient, this may take up to 20 seconds)", ConsoleColor.White);
            context.Database.EnsureDeleted();
            Console.Write("Provisioning new database... (please be patient, this may take up to 60 seconds)", ConsoleColor.White);
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

            // Wait for the actual result
            result.Wait();

            LogUtils.Log("Generating products", ConsoleColor.White);

            GenerateProducts(context);

            // Show a success message
            LogUtils.Success("Database built!");

            // Save the changes to the database context
            context.SaveChanges();



            // Exit the application if configured, when the built process is complete
            if (Program.AppConfig.DatabaseExitAfterReset)
            {
                // Tell the user the application will exit
                LogUtils.Warning("The database has been built. The application will now exit.");

                // Actually exit the application
                Environment.Exit(0);
            }


        }

        private static Product GenerateProduct(string name, string description, double price)
        {
            return new Product()
            {
                Name = name,
                Description = description,
                Price = price
            };
        }

        private static ColorOption GenerateColor(string color, string[] images)
        {
            var co = new ColorOption()
            {
                Color = color
            };

            foreach (string img in images)
            {
                co.Images.Add(new ProductImage(img));
            }

            return co;
        }


        private static void GenerateProducts(ApplicationDbContext context)
        {
            #region Annabelle
            Product annabelle = GenerateProduct(
                "Annabelle Badgley Mischka",
                "Trouwjurk van het merk Badgley & Mischka gemaakt satijn. De top heeft een V-hals en een laag uitgesneden rug, beide afgewerkt met beading. De jurk kan gedragen worden met een cape. De rok heeft een fishtail model met een sleep. De sleep is afgewerkt met tule en is verdeeld in lagen.",
                2500.0d);

            annabelle.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "images/uploads/default/annabelle-1.jpg", "images/uploads/default/annabelle-2.jpg", "images/uploads/default/annabelle-3.jpg" }));

            context.Products.Add(annabelle);
            #endregion

            #region Ladybird
            Product ladybird = GenerateProduct(
                "Ladybird",
                "Trouwjurk van het merk Ladybird gemaakt van kant. De top heeft een hoge doorzichtige neklijn en een laag uitgesneden rug. De rok heeft een fishtail model met een sleep.",
                1250.0d);

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "images/uploads/default/ladybird-ivoor-1.jpg", "images/uploads/default/ladybird-ivoor-2.jpg", "images/uploads/default/ladybird-ivoor-3.jpg" }));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Blauw",
                    new string[] { "images/uploads/default/ladybird-blauw-1.jpg"}));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Wit",
                    new string[] { "images/uploads/default/ladybird-1.jpg", "images/uploads/default/ladybird-2.jpg", "images/uploads/default/ladybird-3.jpg" }));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Roze",
                    new string[] { "images/uploads/default/ladybird-nude-1.jpg", "images/uploads/default/ladybird-nude-2.jpg", "images/uploads/default/ladybird-nude-3.jpg" }));

            context.Products.Add(ladybird);
            #endregion

            #region Pronovias
            Product pronovias = GenerateProduct(
                "Pronovias",
                "Trouwjurk van het merk Pronovias gemaakt van kant. De top is hooggesloten en heeft een laag uitgesneden rug, met lange mouwen. De rok heeft een fishtail model met een sleep.",
                2250.0d);

            pronovias.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "images/uploads/default/pronovias-ivoor-1.jpg", "images/uploads/default/pronovias-ivoor-2.jpg", "images/uploads/default/pronovias-ivoor-3.jpg" }));

            pronovias.ColorOptions.Add(
                GenerateColor(
                    "Wit",
                    new string[] { "images/uploads/default/pronovias-wit-1.jpg", "images/uploads/default/pronovias-wit-2.jpg", "images/uploads/default/pronovias-wit-3.jpg" }));

            context.Products.Add(pronovias);
            #endregion



            context.SaveChanges();
        }
    }
}
