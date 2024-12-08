using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SecureContainer.Models;

namespace SecureContainer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateRolesAndAdminUser();
        }
        private void CreateRolesAndAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Create Admin role if it doesn't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                // Create User role if it doesn't exist
                if (!roleManager.RoleExists("User"))
                {
                    roleManager.Create(new IdentityRole("User"));
                }

                // Create Admin user
                var adminUser = userManager.FindByEmail("admin@example.com");
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                    userManager.Create(adminUser, "Admin@123");
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }

    }
}
