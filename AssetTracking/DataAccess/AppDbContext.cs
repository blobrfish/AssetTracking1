using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using AssetTracking.DataAccess;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AssetTracking.DataAccess
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser> //DbContext
    {
        public const string ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=AssetTrackingApp;Integrated Security=True";
        public DbSet<DataAccess.Office> Offices { get; set; }
        public DbSet<DataAccess.Asset> Assets { get; set; }
        public DbSet<DataAccess.AssetType> AssetTypes { get; set; }
        //public DbSet<DataAccess.ApplicationUser> ApplicationUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext() 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.Options.
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new  OfficeConfig());
            modelBuilder.ApplyConfiguration(new  AssetTypeConfig());
            modelBuilder.ApplyConfiguration(new  AssetConfig());




        }
    }











    /*public abstract class CommonDbContext<TConcreteDBContext, TUser> : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<TUser> where TConcreteDBContext: CommonDbContext<TConcreteDBContext, TUser> where TUser : IdentityUser
    {
        private const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=test;Integrated Security=SSPI;";

     
        public CommonDbContext(DbContextOptions<TConcreteDBContext> options) : base(options)
        {
        }
        public CommonDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Employer.ProfileConfig());
            modelBuilder.ApplyConfiguration(new JobSeeker.Profile.ProfileConfig());
            modelBuilder.ApplyConfiguration(new Common.ContinentConfig());
            modelBuilder.ApplyConfiguration(new Common.CountryConfig());
        }
    }/*
   // public class AppDbContext : DbContext //Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
   // {

   //     private const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=test;Integrated Security=SSPI;";
   ///* public DbSet<JobSeekerAccount.JobSeekerAccount> JobSeekerAccounts { get; set; }
   //     public DbSet<EmployerAccount.EmployerAccount> EmployerAccounts { get; set; }
   //     public DbSet<JobPost.JobPost> JobPosts { get; set; }
   //     public DbSet<JobPost.Relevances.Relevance> JobPostRelevances { get; set; }
   //     public DbSet<JobSearchPost.JobSearchPost> JobSearchPosts { get; set; }

   //     public DbSet<BaseModels. Match.Match> Matches { get; set; }
   //     public DbSet<JobSearchPost.Relevances.Relevance> JobSearchPostRelevances { get; set; }

   //     public DbSet<BaseModels.Match.JobPostRelevanceReply> JobPostRelevanceReplies { get; set; }
   //     public DbSet<BaseModels.Match.JobSearchPostRelevanceReply> JobSearchPostRelevanceReplies { get; set; }

   //     public DbSet<RoleFilterCategoryGroup> RoleFilterCategoryGroups { get; set; }
   //     public DbSet<RoleFilterCategory> RoleFilterCategories { get; set; }
   //     public DbSet<RoleFilterExperienceLevelUnit> RoleFilterExperienceLevelUnits { get; set; }
   //     public DbSet<HoursPerWeek> HoursPerWeek { get; set; }
   //     public DbSet<Currency> Currencies { get; set; }
   //     public DbSet<PayPeriod> PayPeriods { get; set; }
   //     public DbSet<RelevanceCategory.RelevanceCategory> RelevanceCategories { get; set; }
   //     public DbSet<RelevanceFilterImportanceLevel> RelevanceFilterImportanceLevels { get; set; }
   //     public DbSet<RelevanceMetric> RelevanceMetrics { get; set; }
   //     public DbSet<RelevanceMetricUnit> RelevanceMetricUnits { get; set; }
   //     public DbSet<Regions.Continent> Continents { get; set; }
   //     public DbSet<Regions.Country> Countries { get; set; }
   //     public DbSet<BaseModels.Account.AccountLanguage> AccountLanguages { get; set; }

   //     public DbSet<PayFilter> PayFilters { get; set; }

   //     public DbSet<AreaSize> AreaSizes { get; set; }*/

   //      public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
   //     {
   //     }

   //     public AppDbContext() : base()
   //     {
   //     }

   //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //     { 
   //         optionsBuilder.UseSqlServer(ConnectionString);
   //         //base.OnConfiguring(optionsBuilder);
   //     }
   //     protected override void OnModelCreating(ModelBuilder modelBuilder)
   //     {

   //         modelBuilder.ApplyConfiguration(new Common.ContinentConfig());
   //         modelBuilder.ApplyConfiguration(new Common.CountryConfig());

   //         /*modelBuilder.ApplyConfiguration(new EmployerAccount.EmployerAccuntConfig());
   //         modelBuilder.ApplyConfiguration(new EmployerAccount.EmployerProfile.EmployerProfileConfig());
   //         modelBuilder.ApplyConfiguration(new JobPost.JobPostConfig());

   //         modelBuilder.ApplyConfiguration(new JobPost.Category.CategoryConfig());
   //         modelBuilder.ApplyConfiguration(new AreaSizeConfig());
   //         modelBuilder.ApplyConfiguration(new JobPost.Relevances.RelevanceConfig());
   //         modelBuilder.ApplyConfiguration(new JobSeekerAccount.JobSeekerAccuntConfig());
   //         modelBuilder.ApplyConfiguration(new JobSeekerAccount.JobSeekerProfile.JobSeekerProfileConfig());
   //         modelBuilder.ApplyConfiguration(new JobSearchPost.JobSearchPostConfig());
   //         modelBuilder.ApplyConfiguration(new JobSearchPost.Category.CategoryConfig());
   //         modelBuilder.ApplyConfiguration(new JobSearchPost.Relevances.RelevanceConfig());
   //         modelBuilder.ApplyConfiguration(new BaseModels. Match.MatchConfig());
   //         modelBuilder.ApplyConfiguration(new BaseModels.Match.JobSearchPostRelevanceReplyConfig());
   //         modelBuilder.ApplyConfiguration(new BaseModels.Match.JobPostRelevanceReplyConfig());
   //         modelBuilder.ApplyConfiguration(new RoleFilterCategoryConfig());
   //         modelBuilder.ApplyConfiguration(new RoleFilterCategoryGroupConfig());
   //         modelBuilder.ApplyConfiguration(new RelevanceCategory.RelevanceCategoryConfig());
 
   //         modelBuilder.ApplyConfiguration(new JobPostRelevanceCategoryFilterMetricOrderConfig());
   //         modelBuilder.ApplyConfiguration(new PayPeriodConfig());
   //         modelBuilder.ApplyConfiguration(new CurrencyConfig());
   //         modelBuilder.ApplyConfiguration(new JobSearchPostRelevanceFilterMetricConfig());

   //         modelBuilder.ApplyConfiguration(new PayFilterConfig());
   //         modelBuilder.ApplyConfiguration(new RelevanceMetricConfig());
   //         modelBuilder.ApplyConfiguration(new RelevanceMetricUnitConfig());
   //         modelBuilder.ApplyConfiguration(new RoleFilterExperienceLevelConfig());

   //         modelBuilder.ApplyConfiguration(new RoleFilterExperienceLevelUnitConfig());
   //         modelBuilder.ApplyConfiguration(new RelevanceFilterImportanceLevelConfig());
   //         modelBuilder.ApplyConfiguration(new HoursPerWeekConfig());
   //         modelBuilder.ApplyConfiguration(new JobPost.SettingsConfig());
   //        // modelBuilder.ApplyConfiguration(new JobSearchPost.SettingsSectionConfig());
   //         //modelBuilder.ApplyConfiguration(new JobPost.SettingsSectionElementConfig());
   //         modelBuilder.ApplyConfiguration(new JobSearchPost.SettingsConfig());
   //         modelBuilder.ApplyConfiguration(new Regions.ContinentConfig());
   //         modelBuilder.ApplyConfiguration(new Regions.CountryConfig());
   //         modelBuilder.ApplyConfiguration(new BaseModels.Account.AccountLanguageConfig());

   //         //modelBuilder.ApplyConfiguration(new JobSearchPost.SettingsSectionConfig());
   //         //modelBuilder.ApplyConfiguration(new JobSearchPost.SettingsSectionElementConfig());*/



   //     }
   // }

   

  
}






