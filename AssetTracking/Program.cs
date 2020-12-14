using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using AssetTracking.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AssetTracking
{
    partial class Program : IDesignTimeDbContextFactory<DataAccess.AppDbContext>
    {
        private static DataAccess.Repository Repository;
        public static ServiceCollection Services;
        static void Main(string[] args)
        {
            
            Services = new ServiceCollection();
            Services.AddDbContext<AppDbContext>(
           options =>
           {
               options.UseSqlServer(AppDbContext.ConnectionString);
           });

            // Authentification
            Services.AddIdentityCore<ApplicationUser>(options =>
            {
                // Configure identity options
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;


            })
                .AddEntityFrameworkStores<AppDbContext>();
            Repository = new DataAccess.Repository();

            PageLogin();

        }




        public static void PageLogin()
        {
            Header("Login");
            Console.WriteLine("Enter n if you want to register as new user");

            Console.WriteLine("Enter user name");
            var nameInput = Console.ReadLine();
            if (nameInput == "n")
            {
                Console.Clear();
                PageRegisterNewUser();
            }
            Console.WriteLine("Enter password");
            var passwordInput = Console.ReadLine();
            bool isSuccesful = Repository.CheckLoginCredentials(nameInput, passwordInput);
            if(isSuccesful)
            {
                PageMainMenu();
            }
            else
            {
                Console.WriteLine("Username or password was incorrect");
                Console.ReadLine();
                Console.Clear();
                PageLogin();
            }
            
        }

        public static void PageRegisterNewUser()
        {
            Header("Register new user");
            Console.WriteLine("Enter c if you want to cancel registration");
            Console.WriteLine("Enter user name");
            var nameInput = Console.ReadLine();
            if (nameInput == "c")
            {
                Console.Clear();
                PageLogin();
            }
            Console.WriteLine("Enter password");
            var passwordInput = Console.ReadLine();
            bool isSuccesful = Repository.CreateUser(nameInput, passwordInput);
            if (!isSuccesful)
            {
                Console.WriteLine("Registration failed");
                Console.ReadLine();
                Console.Clear();
                PageRegisterNewUser();
            }
            Console.WriteLine("Registration was created succesfully");
            Console.ReadLine();
            PageLogin();
        }

       
        private static void PageMainMenu()
        {
            Header("Huvudmeny");

            //ShowAllBlogPostsBrief();

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("a) Read all company assets");
            Console.WriteLine("b) Add a new office");
            Console.WriteLine("c) Delete an office");
            Console.WriteLine("d) Add a new asset");
            Console.WriteLine("e) Delete an asset");
            Console.WriteLine("f) Add a new asset type");
            Console.WriteLine("g) Delete asset type");
            Console.WriteLine("h) Logout");


            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.A)
                PageReadAllCompanyAssets();

            if (command == ConsoleKey.B)
                PageAddNewOffice();

            if (command == ConsoleKey.C)
                PageDeleteOffice();

            if (command == ConsoleKey.D)
                PageAddAsset();

            if (command == ConsoleKey.E)
                PageDeleteAsset();

            if (command == ConsoleKey.F)
                PageAddNewAssetType();

            if (command == ConsoleKey.G)
                PageDeleteAssetType();

            if (command == ConsoleKey.H)
                Logout();
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

        private static void Logout()
        {
            Console.Clear();
            PageLogin();
        }

        public static void PageReadAllCompanyAssets()
        {
            Repository = new DataAccess.Repository();
            foreach (Office a in Repository.AllCompanyAssets())
                a.WriteAsset();
        }

        public static void PageDeleteOffice()
        {
            Console.WriteLine(string.Format("Enter the id of the office you want to delete"));
            foreach (Office o in Repository.AllOffices())
                Console.WriteLine(string.Format("{0}) {1}",o.Id, o.Location));
            var enteredId = Convert.ToInt32(Console.ReadLine());
            Repository.DeleteOffice(enteredId);
        }

        public static void PageAddNewOffice()
        {
          
            Console.WriteLine("Enter the location");
            var locationInput = Console.ReadLine();
            Console.WriteLine("Enter the local currency code");
            var localCurrencyCodeInput = Console.ReadLine();
            var newOffice = new Office(locationInput, localCurrencyCodeInput);
            Repository.AddOffice(newOffice);
        }

        public static void PageAddNewAssetType()
        {
            Console.WriteLine("Enter the name");
            var nameInput = Console.ReadLine();
            var newAssetType = new AssetType(nameInput);
            Repository.AddAssetType(newAssetType);
        }
        public static void PageAddAsset()
        {
            Console.WriteLine(string.Format("Enter the id of the office you want to add an asset to"));
            foreach (var o in Repository.AllOffices())
                Console.WriteLine(string.Format("{0}) {1}", o.Id, o.Location));
            var officeIdInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(string.Format("Enter the id of the asset type"));
            foreach (var o in Repository.AllAssetTypes())
                Console.WriteLine(string.Format("{0}) {1}", o.Id, o.Name));
            var assetTypeIdInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the model name");
            var modelNameInput = Console.ReadLine();
            Console.WriteLine("Enter price in dollars");
            var priceInDollarsInput = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter purchase date in format YYYY-MM-DD or leave it empty for todays date");
            var purchaseDateInput = Console.ReadLine() == string.Empty ? DateTime.Today : Convert.ToDateTime(Console.ReadLine());

            var asset= new AssetTracking.Asset(assetTypeIdInput, modelNameInput, purchaseDateInput, priceInDollarsInput);
            Repository.AddAsset(officeIdInput, asset);
        }


        public static void PageDeleteAsset()
        {
            
            PageReadAllCompanyAssets();
            Console.WriteLine(string.Format("Enter the id of the asset you want to delete"));
            var idInput = Convert.ToInt32(Console.ReadLine());
            Repository.DeleteAsset(idInput);
        }

        public static void PageDeleteAssetType()
        {

            PageReadAllCompanyAssets();
            Console.WriteLine(string.Format("Enter the id of the asset type you want to delete"));
            var idInput = Convert.ToInt32(Console.ReadLine());
            Repository.DeleteAssetType(idInput);
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

    public interface IUserCreationService
    {
        Task CreateUser();
    }


    //public class UserCreationService : IUserCreationService
    //{
    //    public readonly UserManager<ApplicationUser> UserManager;

    //    public UserCreationService(UserManager<ApplicationUser> userManager)
    //    {
    //        this.userManager = userManager;
    //    }

    //    public async Task CreateUser()
    //    {
    //        var user = new ApplicationUser { UserName = "TestUser", Email = "test@example.com" };
    //        var result = await this.userManager.CreateAsync(user, "123456");

    //        if (result.Succeeded == false)
    //        {
    //            foreach (var error in result.Errors)
    //            {
    //                Console.WriteLine(error.Description);
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine("Done.");
    //        }
    //    }
    //}
}

