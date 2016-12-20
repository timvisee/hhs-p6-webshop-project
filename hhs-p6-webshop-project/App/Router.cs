using System;
using hhs_p6_webshop_project.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace hhs_p6_webshop_project.App {
    public class Router {
        /// <summary>
        /// Set up the router for MVC.
        /// </summary>
        /// <param name="app">Application builder instance to set up the routes for.</param>
        public void SetUp(IApplicationBuilder app) {
            // Configure the MVC routes
            app.UseMvc(ConfigureRoutes());
        }

        /// <summary>
        /// Configure the actual routes, as an action with route builders.
        /// </summary>
        /// <returns>Action with route builders</returns>
        private static Action<IRouteBuilder> ConfigureRoutes() {
            return routes => {
                // Default router
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {
                        controller = "Home",
                        action = "Index"
                    }
                );
            };
        }
    }
}