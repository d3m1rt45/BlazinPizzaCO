namespace BlazinPizzaCO.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlazinPizzaCO.DAL.BlazinContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlazinPizzaCO.DAL.BlazinContext context)
        {
            
        }
    }
}
