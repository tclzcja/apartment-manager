using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Models.ProfileModel> Profiles { get; set; }
        public DbSet<Models.ApartmentModel> Apartments { get; set; }
        public DbSet<Models.BuildingModel> Buildings { get; set; }
        public DbSet<Models.RequestModel> Requests { get; set; }

        public AppDbContext() : base("LocalConnection") { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}