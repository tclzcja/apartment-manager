using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class BuildingModel
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<ApartmentModel> Apartments { get; set; }
        public virtual List<ProfileModel> Superintendents { get; set; }
    }
}