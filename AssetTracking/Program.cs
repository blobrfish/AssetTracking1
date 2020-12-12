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
        private static DataAccess.Repository Repository;

        static void Main(string[] args)
        {
            Repository = new DataAccess.Repository();
            if(LogIn())
            {
                PageMainMenu();
            }
               //PrintOfficeAssetsToConsole(Offices);
        }

        private static bool LogIn()
        {
            return true;
            //Console.Clear();

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine();
            //Console.WriteLine(text.ToUpper());
            //Console.WriteLine();
        }
        private static void PageMainMenu()
        {
            Header("Huvudmeny");

            //ShowAllBlogPostsBrief();

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("a) Read all company assets");
            Console.WriteLine("b) Add a new office");
            Console.WriteLine("c) Delete an exisitng office");
            Console.WriteLine("d) Add a new asset");
            Console.WriteLine("e) Delete an existing asset");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.A)
                PageReadAllCompanyAssets();

            //if (command == ConsoleKey.B)
            //    PageUpdatePost();

            //if (command == ConsoleKey.A)
            //    PageMainMenu();

            //if (command == ConsoleKey.B)
            //    PageUpdatePost();
        }

       
        //public static void WriteLine(string text = "")
        //{
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine(text);
        //}
        private static void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }

      

        private bool LogOut()
        {
            return true;
        }

        public static void PageReadAllCompanyAssets()
        {
            foreach (Office a in Repository.AllCompanyAssets())
                a.WriteAsset();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(AppDbContext.ConnectionString);
            return new AppDbContext(builder.Options);
        }

        ////Factories
        //public static IEnumerable<Asset> Assets
        //{
        //    get
        //    {
        //        var assets = new List<Asset>();
        //        assets.Add(new MobilePhone("Nokia 3310", new DateTime(2018, 02, 22), 490));
        //        assets.Add(new MobilePhone("IPhone 10", new DateTime(2020, 10, 21), 790));
        //        assets.Add(new LaptopComputer("e510", new DateTime(2018, 03, 26), 890));
        //        assets.Add(new MobilePhone("3210", new DateTime(2016, 12, 21), 490));
        //        assets.Add(new MobilePhone("7", new DateTime(2017, 12, 21), 790));
        //        assets.Add(new LaptopComputer("Zen book 7", new DateTime(2018, 04, 21), 890));
        //        return assets;
        //    }
        //}

        //public static IEnumerable<Office> Offices
        //{
        //    get
        //    {
        //        var items = new List<Office>();
        //        items.Add(new Office("Michigan", Assets,"USD"));
        //        items.Add(new Office("London", Assets, "GBP"));
        //        items.Add(new Office("Milano",Assets, "EUR"));
        //        return items;
        //    }
        //}
    }
}

