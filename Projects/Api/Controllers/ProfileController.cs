using Api.Context;
using Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    public class ProfileController : ApiController
    {
        AMDbContext DB;

        public async Task<HttpResponseMessage> Register([FromBody]ProfileModel value)
        {
            DB = new AMDbContext();
            var AUM = new ApplicationUserManager(new UserStore<IdentityUser>(DB));

            var u = new IdentityUser(value.User.Email);

            await AUM.CreateAsync(u, "default");

            var profile = new ProfileModel()
            {
                User = u,
                Name = value.Name,
                Role = value.Role
            };

            DB.Profiles.Add(profile);

            DB.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
