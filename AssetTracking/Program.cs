using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
namespace AssetTracking
{
    partial class Program : IDesignTimeDbContextFactory<AppDbContext>
    {
        private static AppDbContext DbContext;
        static void Main(string[] args)
        {
            var provider = new Provider();
               PrintOfficeAssetsToConsole(Offices);
        }

      

        public static void PrintOfficeAssetsToConsole(IEnumerable<Office> offices)
        {
            foreach (Office a in offices)
                a.PrintAssetsToConsole();
        }


        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=AssetTrackingApp;Integrated Security=True");
            return new AppDbContext(builder.Options);
        }

        //Factories
        public static IEnumerable<Asset> Assets
        {
            get
            {
                var assets = new List<Asset>();
                assets.Add(new MobilePhone("Nokia 3310", new DateTime(2018, 02, 22), 490));
                assets.Add(new MobilePhone("IPhone 10", new DateTime(2020, 10, 21), 790));
                assets.Add(new LaptopComputer("e510", new DateTime(2018, 03, 26), 890));
                assets.Add(new MobilePhone("3210", new DateTime(2016, 12, 21), 490));
                assets.Add(new MobilePhone("7", new DateTime(2017, 12, 21), 790));
                assets.Add(new LaptopComputer("Zen book 7", new DateTime(2018, 04, 21), 890));
                return assets;
            }
        }

        public static IEnumerable<Office> Offices
        {
            get
            {
                var items = new List<Office>();
                items.Add(new Office("Michigan", Assets,"USD"));
                items.Add(new Office("London", Assets, "GBP"));
                items.Add(new Office("Milano",Assets, "EUR"));
                return items;
            }
        }
    }
}

