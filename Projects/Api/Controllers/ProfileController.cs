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

namespace Api.Controllers
{
    public class ProfileActionApartmentModel
    {
        public string ProfileID { get; set; }
        public string ApartmentID { get; set; }
    }

    public class ProfileActionBuildingModel
    {
        public string ProfileID { get; set; }
        public string BuildingID { get; set; }
    }

    [AllowAnonymous]
    public class ProfileController : ApiController
    {
        AppDbContext DB;

        [HttpPost]
        [Route("api/profile/register")]
        public async Task<HttpResponseMessage> Register([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();
            var AUM = new ApplicationUserManager(new UserStore<IdentityUser>(DB));

            var u = new IdentityUser(value.User.Email);

            await AUM.CreateAsync(u, "default");

            var profile = new ProfileModel()
            {
                ID = Guid.NewGuid().ToString(),
                User = u,
                Name = value.Name,
                Apartments = new List<ApartmentModel>(),
                Buildings = new List<BuildingModel>()
            };

            DB.Profiles.Add(profile);

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/profile/get")]
        public async Task<List<ProfileModel>> Get()
        {
            DB = new AppDbContext();

            return await DB.Profiles.ToListAsync();
        }

        [HttpGet]
        [Route("api/profile/get/{id}")]
        public async Task<ProfileModel> Get(string id)
        {
            DB = new AppDbContext();

            return await DB.Profiles.SingleAsync(p => p.ID == id);
        }

        [HttpPost]
        [Route("api/profile/assign/apartment")]
        public async Task<HttpResponseMessage> AssignApartment([FromBody]ProfileActionApartmentModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ProfileID);
            var A = await DB.Apartments.SingleAsync(a => a.ID == value.ApartmentID);

            if (!P.Apartments.Contains(A))
            {
                P.Apartments.Add(A);
                A.Tenants.Add(P);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/assign/building")]
        public async Task<HttpResponseMessage> AssignBuilding([FromBody]ProfileActionBuildingModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ProfileID);
            var B = await DB.Buildings.SingleAsync(b => b.ID == value.BuildingID);

            if (!P.Buildings.Contains(B))
            {
                P.Buildings.Add(B);
                B.Superintendents.Add(P);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/remove/apartment")]
        public async Task<HttpResponseMessage> RemoveApartment([FromBody]ProfileActionApartmentModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ProfileID);
            var A = await DB.Apartments.SingleAsync(a => a.ID == value.ApartmentID);

            if (P.Apartments.Contains(A))
            {
                P.Apartments.Remove(A);
                A.Tenants.Remove(P);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/profile/remove/building")]
        public async Task<HttpResponseMessage> RemoveBuilding([FromBody]ProfileActionBuildingModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ProfileID);
            var B = await DB.Buildings.SingleAsync(b => b.ID == value.BuildingID);

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
