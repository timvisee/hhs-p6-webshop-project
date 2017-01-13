using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Http.Features;

namespace hhs_p6_webshop_project.Data
{
    public class DressBuilder {
        private static bool initDone = false;

        private static PropertyType kleur;
        private static PropertyType prijs;

        static void init(ApplicationDbContext context) {
            if (initDone) return;
             kleur = new PropertyType();
            kleur.DataType = typeof(string).FullName;
            kleur.Multiple = true;
            kleur.Name = "Kleur";
            kleur.Required = true;
            context.PropertyType.Add(kleur);

            prijs = new PropertyType();
            prijs.DataType = typeof(double).FullName;
            prijs.Multiple = false;
            prijs.Name = "Prijs";
            prijs.Required = true;
            context.PropertyType.Add(prijs);

            initDone = true;
        }

        public static void Build(ApplicationDbContext context, string name, string description, string image, double prijs, string kleur) {
            init(context);
            
            Product dress = new Product();
            dress.Name = name;
            dress.Description = description;

            context.Product.Add(dress);
            
            ProductType pt = new ProductType();
            pt.Images.Add(new ProductImage(image));
            pt.PropertyValueCouplings.Add(new PropertyValueCoupling(pt, DressBuilder.prijs, new PropertyValue(prijs)));
            pt.PropertyValueCouplings.Add(new PropertyValueCoupling(pt, DressBuilder.kleur, new PropertyValue(kleur)));

            dress.ProductTypes.Add(pt);

            context.SaveChanges();

        }

    }
}
