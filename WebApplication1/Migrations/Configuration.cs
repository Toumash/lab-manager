using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LabContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "admin"});
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "superadmin"});
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "mod"});

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin"))
            {
                var user = new ApplicationUser {UserName = "admin", Email = "admin@domena.pl"};
                var result = userManager.Create(user, "passW0rd!");

                if (result.Succeeded)
                {
                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "admin"});
                    context.SaveChanges();
                    userManager.AddToRole(user.Id, "admin");
                }
                else
                {
                    throw new Exception("Cannot create the user");
                }
            }
        }
    }
}