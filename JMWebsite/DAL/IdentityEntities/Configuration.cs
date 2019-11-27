namespace JMWebsite.DAL.IdentityEntities
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JMWebsite.DAL.IdentityEntities.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\IdentityEntities";
        }

        protected override void Seed(JMWebsite.DAL.IdentityEntities.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it doesn't already exist
            if(!context.Roles.Any(r=> r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            //Create Role Manager if it doesn't already exist
            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Manager"));
            }

            //Creates a user manager
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Create admin user
            var adminuser = new ApplicationUser
            {
                UserName = "admin1@outlook.com",
                Email = "admin1@outlook.com"
            };

            //Assign the admin user to the role
            if(!context.Users.Any(u=>u.UserName =="admin1@outlook.com"))
            {
                manager.Create(adminuser, "password");
                manager.AddToRole(adminuser.Id, "Admin");
            }

            //Create manager user
            var manageruser = new ApplicationUser
            {
                UserName = "manager1@outlook.com",
                Email = "manager1@outlook.com"
            };

            //Assign the manager user to the role
            if (!context.Users.Any(u => u.UserName == "manager1@outlook.com"))
            {
                manager.Create(manageruser, "password");
                manager.AddToRole(manageruser.Id, "Manager");
            }

            //regular user
            var user = new ApplicationUser
            {
                UserName = "user1@outlook.com",
                Email = "user1@outlook.com"
            };

            if(!context.Users.Any(u=>u.UserName == "user1@outlook.com"))
            {
                manager.Create(user, "password");
            }
        }
    }
}
