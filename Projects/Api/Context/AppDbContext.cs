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

        public AppDbContext() : base("DefaultConnection") { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}