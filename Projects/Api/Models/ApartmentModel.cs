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
        public string BuildingID { get; set; }
        [ForeignKey("BuildingID")]
        public BuildingModel Building { get; set; }
        public int Number { get; set; }
    }
}