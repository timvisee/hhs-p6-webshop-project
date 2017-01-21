using System;
using System.Collections.Generic;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Models.NewsModels;
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

            GenerateNews(context);

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

        private static void GenerateNews(ApplicationDbContext context)
        {
            #region Articles
            NewsArticle article1 = new NewsArticle()
            {
                Name = "Collectie",
                ImagePath = "//images/uploads/default/ladybird-1.jpg",
                Excerpt = "De super sexy collectie van Martina Liana is gearriveerd",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article2 = new NewsArticle()
            {
                Name = "Lovely brides",
                ImagePath = "/images/appointment-thankyou.jpg",
                Excerpt = "Rachelle & Matthew",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article3 = new NewsArticle()
            {
                Name = "Nieuws",
                ImagePath = "/images/home-banner-main.jpg",
                Excerpt = "Het nieuwe magazine is uit! Vraag hem gratis aan.",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article4 = new NewsArticle()
            {
                Name = "Artikel",
                ImagePath = "/images/home-banner-footer.jpg",
                Excerpt = "Van pinterest droom naar werkelijkheid",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article5 = new NewsArticle()
            {
                Name = "Een nieuwe dressfinder",
                ImagePath = "/images/dressfinder-banner.jpg",
                Excerpt = "Bekijk nu onze nieuwe dressfinder!",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article6 = new NewsArticle()
            {
                Name = "Bem de CSS Koning",
                ImagePath = "/images/appointment/appointment-banner.png",
                Excerpt = "Donderdag 19 januari 2017 is Bem verkozen tot CSS koning",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article7 = new NewsArticle()
            {
                Name = "Bar(re)man Henk",
                ImagePath = "/images/dressfinder-banner-top.png",
                Excerpt = "Het is over het algemeen bekend, Henk is een barre man!",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article8 = new NewsArticle()
            {
                Name = "Koffiebaas Bin",
                ImagePath = "/images/appointment-thankyou.jpg",
                Excerpt = "Bin haal eens koffie! En 5 minuten later heb je 3x 5ml",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };

            NewsArticle article9 = new NewsArticle()
            {
                Name = "Wijsheden",
                ImagePath = "//images/uploads/default/ladybird-1.jpg",
                Excerpt = "Laten we een voorbeeld nemen aan de schijf van 5: K, G, F, M, P",
                Content = "<p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p><p>Wat is Lorem Ipsum?\r\nLorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren \'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.</p>",
            };
            #endregion

            #region Categories
            NewsCategory cat1 = new NewsCategory()
            {
                Name = "Collectie"
            };

            NewsCategory cat2 = new NewsCategory()
            {
                Name = "Evenement"
            };

            NewsCategory cat3 = new NewsCategory()
            {
                Name = "Nieuws"
            };

            NewsCategory cat4 = new NewsCategory()
            {
                Name = "Lovely real brides"
            };
            #endregion

            #region Couplings
            NewsArticleCategory nac1 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article1
            };


            NewsArticleCategory nac2 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article2
            };

            NewsArticleCategory nac3 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article3
            };

            NewsArticleCategory nac4 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article4
            };

            NewsArticleCategory nac5 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article5
            };

            NewsArticleCategory nac6 = new NewsArticleCategory()
            {
                NewsCategory = cat1,
                NewsArticle = article6
            };

            NewsArticleCategory nac7 = new NewsArticleCategory()
            {
                NewsCategory = cat2,
                NewsArticle = article6
            };

            NewsArticleCategory nac8 = new NewsArticleCategory()
            {
                NewsCategory = cat3,
                NewsArticle = article6
            };

            NewsArticleCategory nac9 = new NewsArticleCategory()
            {
                NewsCategory = cat3,
                NewsArticle = article2
            };

            NewsArticleCategory nac10 = new NewsArticleCategory()
            {
                NewsCategory = cat3,
                NewsArticle = article3
            };

            NewsArticleCategory nac11 = new NewsArticleCategory()
            {
                NewsCategory = cat4,
                NewsArticle = article4
            };

            NewsArticleCategory nac12 = new NewsArticleCategory()
            {
                NewsCategory = cat4,
                NewsArticle = article6
            };

            NewsArticleCategory nac13 = new NewsArticleCategory()
            {
                NewsCategory = cat4,
                NewsArticle = article7
            };

            NewsArticleCategory nac14 = new NewsArticleCategory()
            {
                NewsCategory = cat2,
                NewsArticle = article8
            };

            NewsArticleCategory nac15 = new NewsArticleCategory()
            {
                NewsCategory = cat3,
                NewsArticle = article9
            };
            #endregion

            #region Fill context
            context.NewsArticle.Add(article1);
            context.NewsArticle.Add(article2);
            context.NewsArticle.Add(article3);
            context.NewsArticle.Add(article4);
            context.NewsArticle.Add(article5);
            context.NewsArticle.Add(article6);
            context.NewsArticle.Add(article7);
            context.NewsArticle.Add(article8);
            context.NewsArticle.Add(article9);

            context.NewsCategory.Add(cat1);
            context.NewsCategory.Add(cat2);
            context.NewsCategory.Add(cat3);
            context.NewsCategory.Add(cat4);

            context.NewsArticleCategory.Add(nac1);
            context.NewsArticleCategory.Add(nac2);
            context.NewsArticleCategory.Add(nac3);
            context.NewsArticleCategory.Add(nac4);
            context.NewsArticleCategory.Add(nac5);
            context.NewsArticleCategory.Add(nac6);
            context.NewsArticleCategory.Add(nac7);
            context.NewsArticleCategory.Add(nac8);
            context.NewsArticleCategory.Add(nac9);
            context.NewsArticleCategory.Add(nac10);
            context.NewsArticleCategory.Add(nac11);
            context.NewsArticleCategory.Add(nac12);
            context.NewsArticleCategory.Add(nac13);
            context.NewsArticleCategory.Add(nac14);
            context.NewsArticleCategory.Add(nac15);
            #endregion

            context.SaveChanges();
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
                    new string[] { "/images/uploads/default/annabelle-1.jpg", "/images/uploads/default/annabelle-2.jpg", "/images/uploads/default/annabelle-3.jpg" }));

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
                    new string[] { "/images/uploads/default/ladybird-ivoor-1.jpg", "/images/uploads/default/ladybird-ivoor-2.jpg", "/images/uploads/default/ladybird-ivoor-3.jpg" }));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Blauw",
                    new string[] { "/images/uploads/default/ladybird-blauw-1.jpg" }));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Wit",
                    new string[] { "/images/uploads/default/ladybird-1.jpg", "/images/uploads/default/ladybird-2.jpg", "/images/uploads/default/ladybird-3.jpg" }));

            ladybird.ColorOptions.Add(
                GenerateColor(
                    "Roze",
                    new string[] { "/images/uploads/default/ladybird-nude-1.jpg", "/images/uploads/default/ladybird-nude-2.jpg", "/images/uploads/default/ladybird-nude-3.jpg" }));

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
                    new string[] { "/images/uploads/default/pronovias-ivoor-1.jpg", "/images/uploads/default/pronovias-ivoor-2.jpg", "/images/uploads/default/pronovias-ivoor-3.jpg" }));

            pronovias.ColorOptions.Add(
                GenerateColor(
                    "Wit",
                    new string[] { "/images/uploads/default/pronovias-wit-1.jpg", "/images/uploads/default/pronovias-wit-2.jpg", "/images/uploads/default/pronovias-wit-3.jpg" }));

            context.Products.Add(pronovias);
            #endregion

            #region Mori Lee
            Product morilee = GenerateProduct(
                "Mori Lee",
                "Trouwjurk van Ronald Joyce model Paphos, Glamour japon uitgevoerd in ivoor kleurig organza met rijke bewerking. De volle rok van organza heeft een lange sleep en is onbewerkt. De strapless sweetheart top is volledig bewerkt met steentjes pareltjes en lovertjes. De jurk sluit met een rijgsluiting.",
                2250.0d);

            morilee.ColorOptions.Add(
                GenerateColor(
                    "Nude",
                    new string[] { "/images/uploads/default/morilee-1.jpg", "/images/uploads/default/morilee-2.jpg", "/images/uploads/default/morilee-3.jpg" }));

            context.Products.Add(morilee);
            #endregion

            #region Tres Chic
            Product treschic = GenerateProduct(
                "Tres Chic",
                "Trouwjurk van het merk Tres Chic gemaakt van kant. De top heeft een offshoulder model met een kleine mouwtje. De rok is kort van voor en lang van achter met een sleep.",
                1750.0d);

            treschic.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "/images/uploads/default/treschic-1.jpg" }));

            context.Products.Add(treschic);
            #endregion

            #region Jarice
            Product jarice = GenerateProduct(
                "Jarice",
                "Trouwjurk van het merk Jarice gemaakt van kant. De top is strapless met een sweetheart lijn. De taille wordt geaccentueerd door een beaded belt. De rok heeft een A-lijn met een sleep.",
                1500.0d);

            jarice.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "/images/uploads/default/jarice-1.jpg", "/images/uploads/default/jarice-2.jpg" }));

            jarice.ColorOptions.Add(
                GenerateColor(
                    "Wit",
                    new string[] { "/images/uploads/default/jarice-wit-1.jpg", "/images/uploads/default/jarice-wit-2.jpg" }));

            context.Products.Add(jarice);
            #endregion

            #region Emma Charlotte
            Product emmacharlotte = GenerateProduct(
                "Emma Charlotte",
                "Trouwjurk van het merk Emma Charlotte gemaakt van kant. De top heeft een hoge doorzichtige neklijn en een laag uitgesneden rug, afgewerkt met kanten applicaties. De jurk heeft een A-lijn met een sleep.",
                1750.0d);

            emmacharlotte.ColorOptions.Add(
                GenerateColor(
                    "Ivoor",
                    new string[] { "/images/uploads/default/emmacharlotte-1.jpg", "/images/uploads/default/emmacharlotte-2.jpg" }));

            context.Products.Add(emmacharlotte);
            #endregion

            context.SaveChanges();
        }
    }
}