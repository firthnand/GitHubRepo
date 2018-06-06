using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HouseWaterMeters.Models
{
    public class House
    {
        [Key]
        public long HouseId { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string HouseAdress { get; set; }
  
        public virtual WaterMeter WaterMeter { get; set; }
    }
}