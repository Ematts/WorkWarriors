namespace WorkWarriors.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkWarriors.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorkWarriors.Models.ApplicationDbContext context)
        {
            context.Homeowners.AddOrUpdate(i => i.Username,
                new Homeowner
        {
            Username = "Agaveman",
            FirstName = "Bob",
            LastName = "Smith",
            Address = "555 W. Center St.",
            City = "Milwaukee",
            State = "WI",
            Zip = "53216", 
            email = "bobsmith@gmail.com"
        }

      );
    }
  }
}
