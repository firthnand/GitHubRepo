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


    public class WaterMeter
    {
        [Key]
        public long WM_id { get; set; }

        [MaxLength(20)]
        [Index]
        public string WMNumber { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual House WMHouse { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<WaterValue> WaterValues { get; set; }


    }
}