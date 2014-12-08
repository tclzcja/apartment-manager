using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class RequestModel
    {
        [Key]
        public string ID { get; set; }

        public string ApartmentID { get; set; }
        [ForeignKey("ApartmentID")]
        public virtual ApartmentModel Apartment { get; set; }

        public CategoryType Category { get; set; }
        public SubType Sub { get; set; }
        public StatusType Status { get; set; }

        public System.DateTime? RequestTime { get; set; }
        public System.DateTime? ResponseTime { get; set; }

        public string RequestTenantID { get; set; }
        [ForeignKey("RequestTenantID")]
        public virtual ProfileModel RequestTenant { get; set; }

        public string ResponseSuperintenentID { get; set; }
        [ForeignKey("ResponseSuperintenentID")]
        public virtual ProfileModel ResponseSuperintenent { get; set; }

        public System.DateTime? PlannedTime { get; set; }
        public System.DateTime? ActualTime { get; set; }
    }
}