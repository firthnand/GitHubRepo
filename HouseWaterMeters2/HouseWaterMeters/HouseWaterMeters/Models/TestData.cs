using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseWaterMeters.Models
{
    public static class TestData
    {
        private static House[] testHouses = new House[]
        {

           new House {
                 HouseId = 1,
                 HouseAdress = "Красногорск, ул. Пришвина, 1"
                },
           new House {
                 HouseId = 2,
                 HouseAdress = "Красногорск, ул. Пришвина, 3"
                },
           new House {
                 HouseId = 3,
                 HouseAdress = "Красногорск, ул. Пришвина, 5"
                },

        };
        private static WaterMeter[] testWaterMeters = new WaterMeter[]
        {
            new WaterMeter
            {
                WM_id = 1,
                WMNumber = "0100001",
                WMHouse = TestData.houses[0]
                //WaterValues = new List<WaterValue>()
            },
            new WaterMeter
            {
                WM_id = 2,
                WMNumber = "0100002",
                WMHouse = TestData.houses[1]
                //WaterValues = new List<WaterValue>()
                //               WaterValues = new List<WaterValue>() {TestData.waterValues[1], TestData.waterValues[1] }
            },

        };
        private static WaterValue[] testWaterValues = new WaterValue[]
        {
            new WaterValue
            {
                valueID = 1,
                ValueDate = new DateTime(2018, 1, 1),
                Value = 100.52,
                WaterMeter = TestData.waterMeters[0]        
            },
            new WaterValue
            {
                valueID = 2,
                ValueDate = new DateTime(2018, 2, 1),
                Value = 200.52,
                WaterMeter = TestData.waterMeters[0]
            },
            new WaterValue
            {
                valueID = 3,
                ValueDate = new DateTime(2018, 2, 20),
                Value = 300.52,
                WaterMeter = TestData.waterMeters[0]
            },
            new WaterValue
            {
                valueID = 4,
                ValueDate = new DateTime(2018, 1, 1),
                Value = 150.52,
                WaterMeter = TestData.waterMeters[1]
            },
            new WaterValue
            {
                valueID = 5,
                ValueDate = new DateTime(2018, 2, 1),
                Value = 200.52,
                WaterMeter = TestData.waterMeters[1]
            },
        };

        public static House[] houses
        {
            get
            {
                return testHouses;
            }
        }

        public static WaterMeter[] waterMeters
        {
            get
            {
                return testWaterMeters;
            }
        }
        public static WaterValue[] waterValues
        {
            get
            {
                return testWaterValues;
            }
        }
    }
}
