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
    public class BuildingController : ApiController
    {
        AppDbContext DB;

        [HttpPost]
        [Route("api/building/create")]
        public async Task<HttpResponseMessage> Create([FromBody]BuildingModel value)
        {
            DB = new AppDbContext();

            var O = new BuildingModel()
            {
                ID = Guid.NewGuid().ToString(),
                Name = value.Name,
                Address = value.Address,
                Superintendents = new List<ProfileModel>(),
                Apartments = new List<ApartmentModel>()
            };

            if (value.Superintendents.Count > 0)
            {
                O.Superintendents.Add(await DB.Profiles.SingleAsync(p => p.ID == value.Superintendents[0].ID));
            }

            DB.Buildings.Add(O);

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/building/get")]
        public async Task<List<BuildingModel>> Get()
        {
            DB = new AppDbContext();
            return await DB.Buildings.ToListAsync();
        }

        [HttpGet]
        [Route("api/building/get/{id}")]
        public async Task<BuildingModel> Get(string id)
        {
            DB = new AppDbContext();
            return await DB.Buildings.SingleAsync(b => b.ID == id);
        }
    }
}
