using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Api.Context
{
    public class AMDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Models.ProfileModel> Profiles { get; set; }

        public AMDbContext() : base("DefaultConnection") { }

        public static AMDbContext Create()
        {
            return new AMDbContext();
        }
    }
}