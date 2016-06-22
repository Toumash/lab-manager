using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.LabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplication1.Models.LabContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin"))
            {
                var user = new ApplicationUser {UserName = "admin", Email = "admin@domena.pl"};
                var result = userManager.Create(user, "passW0rd!");
                if (result.Succeeded)
                {
                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole
                    {
                        Name = "admin"
                    });
                    context.SaveChanges();
                    userManager.AddToRole(user.Id, "admin");
                }
                else
                {
                    throw new Exception("Cannot create the user");
                }
            }
            //  This method will
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}