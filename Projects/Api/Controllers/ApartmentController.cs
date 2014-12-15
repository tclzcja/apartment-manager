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
    public class ApartmentController : ApiController
    {
        AppDbContext DB;

        [HttpPost]
        [Route("api/apartment/create")]
        public HttpResponseMessage Create([FromBody]ApartmentModel value)
        {
            DB = new AppDbContext();

            var O = new ApartmentModel()
            {
                ID = Guid.NewGuid().ToString(),
                Number = value.Number,
                Tenants = null,
                BuildingID = value.BuildingID,
                Building = DB.Buildings.Single(b => b.ID == value.BuildingID)
            };

            DB.Apartments.Add(O);

            DB.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/apartment/delete")]
        public async Task<HttpResponseMessage> Delete([FromBody]ApartmentModel value)
        {
            DB = new AppDbContext();
            var result = await DB.Apartments.SingleAsync(a => a.ID == value.ID);
            DB.Apartments.Remove(result);
            await DB.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/apartment/multiple")]
        public async Task<List<ApartmentModel>> Multiple()
        {
            DB = new AppDbContext();

            var result = await DB.Apartments.ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/apartment/multiple/profile")]
        public async Task<List<ApartmentModel>> MultipleByProfile([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Apartments.Where(a => a.Building.Superintendents.Where(p => p.ID == value.ID).Count() > 0).ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/apartment/single")]
        public async Task<ApartmentModel> Single([FromBody]ApartmentModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Apartments.SingleAsync(a => a.ID == value.ID);

            return result;
        }

    }
}
