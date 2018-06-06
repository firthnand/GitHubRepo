using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HouseWaterMeters.Models
{
    public class WaterValue
    {
        [Key]
        public long valueID { get; set; }

        public virtual WaterMeter WaterMeter { get; set;  } 

        [Required]
        public DateTime ValueDate { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public double Value { get; set; }
        

    }
}