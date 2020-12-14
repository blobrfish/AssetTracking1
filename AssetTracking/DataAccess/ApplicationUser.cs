using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AssetTracking.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        // public bool IsActive { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public ApplicationUser() : base()
        { }
        public ApplicationUser(string username) : base(username)
        { }
    }

    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //builder.HasData
            //(new ApplicationUser()
            //{
            //    Id = "250ee749-de72-4a55-8a23-374ff65fb675",
            //    Email = "employer@gmail.com",
            //    UserName = "employer@gmail.com",
            //    //PasswordHash = "password1"

            //}
           //);
        }
    }
}
