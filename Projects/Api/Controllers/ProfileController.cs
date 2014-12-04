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
    [AllowAnonymous]
    public class ProfileController : ApiController
    {
        AppDbContext DB;

        [Route("api/profile/register")]
        public async Task<HttpResponseMessage> Register([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();
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

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("api/profile/get")]
        public async Task<JsonResult<List<SlimModel>>> Get()
        {
            DB = new AppDbContext();

            var result = await DB.Profiles.Select(p => new SlimModel() { ID = p.ID, Name = p.Name }).ToListAsync();

            return Json(result);
        }

        [Route("api/profile/test")]
        [HttpGet]
        public JsonResult<string> Test()
        {
            return Json("Oh yes!");
        }
    }
}
