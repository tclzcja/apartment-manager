using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.Models
{
    [NotMapped]
    public class SlimModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}