using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class ApartmentModel
    {
        [Key]
        public string ID { get; set; }
        public int Number { get; set; }

        public string BuildingID { get; set; }
        [ForeignKey("BuildingID")]
        public virtual BuildingModel Building { get; set; }

        public virtual List<ProfileModel> Tenants { get; set; }
    }
}