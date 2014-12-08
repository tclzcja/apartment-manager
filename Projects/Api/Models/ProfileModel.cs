using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ProfileModel
    {
        [Key]
        public string ID { get; set; }
        [ForeignKey("ID")]
        public virtual IdentityUser User { get; set; }

        public string Name { get; set; }

        public virtual List<ApartmentModel> Apartments { get; set; }

        public virtual List<BuildingModel> Buildings { get; set; }
    }
}