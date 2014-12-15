using Api.Context;
using Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [AllowAnonymous]
    public class ProfileController : ApiController
    {
        AppDbContext DB;

        [HttpPost]
        [Route("api/profile/register")]
        public HttpResponseMessage Register([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();
            var AUM = new AppUserManager(new UserStore<IdentityUser>(DB));

            var u = new IdentityUser(value.User.Email);
            u.Email = value.User.Email;
            u.EmailConfirmed = true;

            AUM.Create(u, "default");

            var profile = new ProfileModel()
            {
                ID = Guid.NewGuid().ToString(),
                User = u,
                Name = value.Name,
                Apartments = null,
                Buildings = null,
            };

            DB.Profiles.Add(profile);

            DB.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/multiple")]
        public async Task<List<ProfileModel>> Multiple()
        {
            DB = new AppDbContext();

            var result = await DB.Profiles.ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/profile/single")]
        public async Task<ProfileModel> Single([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Profiles.SingleAsync(p => p.ID == value.ID);

            result.User = await DB.Users.SingleAsync(u => u.Id == result.ID);

            return result;
        }

        [HttpPost]
        [Route("api/profile/single/email")]
        public async Task<ProfileModel> SingleByEmail([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Profiles.SingleAsync(p => p.User.Email == value.User.Email);

            result.User = await DB.Users.SingleAsync(u => u.Email == value.User.Email);

            return result;
        }

        [HttpPost]
        [Route("api/profile/assign/apartment")]
        public async Task<HttpResponseMessage> AssignApartment([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ID);
            var AID = value.Apartments[0].ID;
            var A = await DB.Apartments.SingleAsync(a => a.ID == AID);

            if (!P.Apartments.Contains(A))
            {
                P.Apartments.Add(A);
                A.Tenants.Add(P);
            }

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/assign/building")]
        public async Task<HttpResponseMessage> AssignBuilding([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ID);
            var BID = value.Buildings[0].ID;
            var B = await DB.Buildings.SingleAsync(b => b.ID == BID);

            if (!P.Buildings.Contains(B))
            {
                P.Buildings.Add(B);
                B.Superintendents.Add(P);
            }

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/remove/apartment")]
        public async Task<HttpResponseMessage> RemoveApartment([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ID);
            var A = await DB.Apartments.SingleAsync(a => a.ID == value.Apartments[0].ID);

            if (P.Apartments.Contains(A))
            {
                P.Apartments.Remove(A);
                A.Tenants.Remove(P);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/remove/building")]
        public async Task<HttpResponseMessage> RemoveBuilding([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ID);
            var B = await DB.Buildings.SingleAsync(b => b.ID == value.Buildings[0].ID);

            if (P.Buildings.Contains(B))
            {
                P.Buildings.Remove(B);
                B.Superintendents.Remove(P);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        /*
        [Route("api/profile/test")]
        [HttpGet]
        public HttpResponseMessage Test()
        {
            DB = new AppDbContext();
            var AUM = new ApplicationUserManager(new UserStore<IdentityUser>(DB));

            var u = new IdentityUser("lzsoft@gmail.com");

            AUM.Create(u, "default");

            var profile = new ProfileModel()
            {
                ID = Guid.NewGuid().ToString(),
                User = u,
                Name = "lzsoft@gmail.com",
                Apartments = new List<ApartmentModel>(),
                Buildings = new List<BuildingModel>()
            };

            DB.Profiles.Add(profile);

            DB.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
         * */
    }
}
