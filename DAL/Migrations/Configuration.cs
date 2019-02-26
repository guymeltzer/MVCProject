namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Repositories;
    using Models.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.VideosDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.VideosDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var User1 = new User
            {
                Username = "guymeltzer",
                Password = "1qaz2wsx",
                ConfirmPassword = "1qaz2wsx",
                FirstName = "Guy",
                LastName = "Meltzer",
                Address = "123 Wow Street",
                Email = "guy@thepresidentofthenetherlends.com",
                BirthDate = DateTime.Parse("06/06/1985")
            };



            context.Users.Add(User1);
            context.SaveChanges();
        }
    }
}
