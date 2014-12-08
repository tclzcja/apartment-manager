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
        public HttpResponseMessage Create([FromBody]BuildingModel value)
        {
            DB = new AppDbContext();

            var O = new BuildingModel()
            {
                ID = Guid.NewGuid().ToString(),
                Name = value.Name,
                Address = value.Address,
                Superintendents = value.Superintendents,
                Apartments = null
            };

            if (value.Superintendents.Count > 0)
            {
                var id = value.Superintendents[0].ID;
                O.Superintendents[0] = DB.Profiles.Single(p => p.ID == id);
            }

            DB.Buildings.Add(O);

            DB.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/building/multiple")]
        public async Task<List<BuildingModel>> Multiple()
        {
            DB = new AppDbContext();

            var result = await DB.Buildings.ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/building/single")]
        public async Task<JsonResult<BuildingModel>> Single([FromBody]BuildingModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Buildings.SingleAsync(b => b.ID == value.ID);

            return Json(result);
        }
    }
}
