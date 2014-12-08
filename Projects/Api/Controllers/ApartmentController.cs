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
        public async Task<HttpResponseMessage> Create([FromBody]ApartmentModel value)
        {
            DB = new AppDbContext();

            var O = new ApartmentModel()
            {
                ID = Guid.NewGuid().ToString(),
                Number = value.Number,
                Tenants = new List<ProfileModel>(),
                BuildingID = value.BuildingID,
                Building = await DB.Buildings.SingleAsync(b => b.ID == value.BuildingID)
            };

            DB.Apartments.Add(O);

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/apartment/get")]
        public async Task<List<ApartmentModel>> Get()
        {
            DB = new AppDbContext();
            return await DB.Apartments.ToListAsync();
        }

        [HttpGet]
        [Route("api/apartment/get/{id}")]
        public async Task<ApartmentModel> Get(string id)
        {
            DB = new AppDbContext();
            return await DB.Apartments.SingleAsync(a => a.ID == id);
        }

    }
}
