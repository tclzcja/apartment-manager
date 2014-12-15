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
    public class RequestController : ApiController
    {
        AppDbContext DB;

        [HttpPost]
        [Route("api/request/create")]
        public async Task<HttpResponseMessage> Create([FromBody]RequestModel value)
        {
            DB = new AppDbContext();

            var O = new RequestModel()
            {
                ID = Guid.NewGuid().ToString(),
                Category = value.Category,
                RequestTime = System.DateTime.UtcNow,
                Status = StatusType.New,
                ApartmentID = value.ApartmentID,
                Apartment = await DB.Apartments.SingleAsync(a => a.ID == value.ApartmentID),
                RequestTenantID = value.RequestTenantID,
                RequestTenant = await DB.Profiles.SingleAsync(p => p.ID == value.RequestTenantID),
                PlannedTime = null,
                ActualTime = null,
                ResponseSuperintenentID = null,
                ResponseSuperintenent = null,
                ResponseTime = null
            };

            DB.Requests.Add(O);

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/request/response")]
        public async Task<HttpResponseMessage> Response([FromBody]RequestModel value)
        {
            DB = new AppDbContext();

            var R = await DB.Requests.SingleAsync(r => r.ID == value.ID);

            R.ResponseTime = System.DateTime.UtcNow;
            R.ResponseSuperintenentID = value.ResponseSuperintenentID;
            R.ResponseSuperintenent = await DB.Profiles.SingleAsync(p => p.ID == value.ResponseSuperintenentID);
            R.Status = StatusType.Responsed;
            R.PlannedTime = System.DateTime.UtcNow;

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/request/done")]
        public async Task<HttpResponseMessage> Done([FromBody]RequestModel value)
        {
            DB = new AppDbContext();

            var R = await DB.Requests.SingleAsync(r => r.ID == value.ID);

            R.Status = StatusType.Done;
            R.ActualTime = System.DateTime.UtcNow;

            await DB.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/request/multiple")]
        public async Task<List<RequestModel>> Multiple([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Requests.Where(r => r.RequestTenantID == value.ID).ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/request/multiple/profile")]
        public async Task<List<RequestModel>> MultipleByProfile([FromBody]ProfileModel value)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == value.ID);

            var result = new List<RequestModel>();

            foreach (var b in P.Buildings)
            {
                foreach (var a in b.Apartments)
                {
                    result.AddRange(await DB.Requests.Where(r => r.ApartmentID == a.ID).ToListAsync());
                }
            }

            result = result.OrderBy(x => x.Status).ToList();

            //var result = await DB.Requests.Where(r => r.Apartment.Building.Superintendents.Contains(P)).ToListAsync();

            return result;
        }

        [HttpPost]
        [Route("api/request/single")]
        public async Task<RequestModel> Single([FromBody]RequestModel value)
        {
            DB = new AppDbContext();

            var result = await DB.Requests.SingleAsync(r => r.ID == value.ID);

            return result;
        }
    }
}
