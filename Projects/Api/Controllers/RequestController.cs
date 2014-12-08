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
                Sub = value.Sub,
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
            R.PlannedTime = value.PlannedTime;

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

        [HttpGet]
        [Route("api/request/get")]
        public async Task<List<RequestModel>> Get()
        {
            DB = new AppDbContext();

            return await DB.Requests.ToListAsync();
        }

        [HttpGet]
        [Route("api/request/get/{id}")]
        public async Task<RequestModel> Get(string id)
        {
            DB = new AppDbContext();
            return await DB.Requests.SingleAsync(r => r.ID == id);
        }

        [HttpGet]
        [Route("api/request/get/profile/{profileid}")]
        public async Task<List<RequestModel>> GetByProfile(string profileid)
        {
            DB = new AppDbContext();

            var P = await DB.Profiles.SingleAsync(p => p.ID == profileid);

            return await DB.Requests.Where(r => r.Apartment.Building.Superintendents.Contains(P)).ToListAsync();
        }
    }
}
