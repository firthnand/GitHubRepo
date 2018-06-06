using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HouseWaterMeters.Models
{
    public class HouseManagerDatabaseeInitializer : DropCreateDatabaseAlways<HouseManagerContext>
    {
         protected override void Seed(HouseManagerContext context)
        {
            base.Seed(context);

            foreach (var house in TestData.houses)
            {
                context.Houses.Add(house);
            }

            foreach (var wm in TestData.waterMeters)
            {
                context.WaterMeters.Add(wm);
            }

            foreach (var wv in TestData.waterValues)
            {
                context.WaterValues.Add(wv);
            }

            context.SaveChanges();
        }
    } 
}