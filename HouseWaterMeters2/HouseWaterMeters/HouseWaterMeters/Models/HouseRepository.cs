using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;


namespace HouseWaterMeters.Models
{
    public class HouseRepository : IHouseRepository
    {

        private readonly HouseManagerContext m_context =  new HouseManagerContext();

        public HouseRepository()
        {
            
        }

        public House Find(long id)
        {
            House house = m_context.Houses.Find(id);
            return house;
        }

        public WaterMeter FindWmByHouseID(long id)
        {
            var wmFinded  = m_context.WaterMeters.Find(id);
            return wmFinded;
        }

        public WaterMeter FindWmBySerialNum(string serialNum)
        {
            var wmFinded = m_context.WaterMeters.FirstOrDefault(wm => wm.WMNumber == serialNum);
            return wmFinded;
        }

        public IQueryable<House> GetAll()
        {
            return m_context.Houses;
        }

        public async Task<IEnumerable<House>> GetAllAsync()
        {
            return  await (from b in m_context.Houses
                          select b).ToListAsync();
        }

        public IQueryable<WaterMeter> GetAllWaterMeters()
        {
            return m_context.WaterMeters;
        }


        public House Add(House house)
        {
            m_context.Houses.Add(house);
            m_context.SaveChanges();

            return house;


        }

        public House Delete(long id)
        {
            var house = Find(id);
            if (house == null)
            {
                return null;
            }
            if (house.WaterMeter!=null)
            {
                house.WaterMeter.WaterValues.Clear();
                m_context.WaterMeters.Remove(house.WaterMeter);
            }
            m_context.Houses.Remove(house);

            try
            {
                m_context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return house;
        }

        public House Update(House house)
        {
            var updatedHouse = this.Find(house.HouseId);

            if (house == null)
            {
                return null;
            }
            updatedHouse.HouseAdress = house.HouseAdress;
            m_context.SaveChanges();

            return updatedHouse;
        }

         public WaterMeter AddWaterMeterByHouseID(long houseID, WaterMeter newWaterMeter)
        {
            var house = m_context.Houses.Find(houseID);
            if (house == null)
            {
                return null;
            }

            if (house.WaterMeter != null)
            {
                house.WaterMeter.WaterValues.Clear();
                m_context.WaterMeters.Remove(house.WaterMeter);
            }

            house.WaterMeter = newWaterMeter;
            newWaterMeter.WMHouse = house;
            m_context.WaterMeters.Add(newWaterMeter);
            m_context.SaveChanges();

            return newWaterMeter;

        }

        public House GetMaxConsumedHouse(DateTime startDate, DateTime endDate)
        {
            var result = m_context.WaterValues.Where(x => x.ValueDate >= startDate && x.ValueDate <= endDate)
                .OrderBy(x=>x.ValueDate);
            if (result == null)
            {
                return null;
            }
            IEnumerable<IGrouping<WaterMeter, WaterValue>> query = result.GroupBy(x => x.WaterMeter, x => x);

            double maxConsumption = 0;
            var actualHouse = new House();
            actualHouse = null;

            foreach (IGrouping<WaterMeter, WaterValue> wmGroup in query)
            {
                WaterMeter wm = wmGroup.Key;
                // Iterate over each value in the 
                // IGrouping and print the value.
                WaterValue wv1 = wmGroup.First();
                WaterValue wv2 = wmGroup.Last();
                double consumption = wv2.Value - wv1.Value;
                if (wm.WMHouse != null)
                {
                    if (consumption > maxConsumption)
                    {
                        actualHouse = wm.WMHouse;
                        maxConsumption = consumption;
                    }
                }
            }

            return actualHouse;
        }

        public House GetMinConsumedHouse(DateTime startDate, DateTime endDate)
        {
            var result = m_context.WaterValues.Where(x => x.ValueDate >= startDate && x.ValueDate <= endDate)
                .OrderBy(x => x.ValueDate);
            IEnumerable<IGrouping<WaterMeter, WaterValue>> query = result.GroupBy(x => x.WaterMeter, x => x);

            double minConsumption = 0;
            bool consumptionIntializied = false;
            var actualHouse = new House();
            actualHouse = null;

            foreach (IGrouping<WaterMeter, WaterValue> wmGroup in query)
            {
                WaterMeter wm = wmGroup.Key;
                // Iterate over each value in the 
                // IGrouping and print the value.
                WaterValue wv1 = wmGroup.First();
                WaterValue wv2 = wmGroup.Last();
                double consumption = wv2.Value - wv1.Value;

                if (wm.WMHouse != null)
                {
                    if (!consumptionIntializied)
                    {
                        minConsumption = consumption;
                        actualHouse = wm.WMHouse;
                        consumptionIntializied = true;
                    }
                    else if (consumption < minConsumption)
                    {
                        actualHouse = wm.WMHouse;
                        minConsumption = consumption;
                    }
                }
            }

            return actualHouse;
        }


        public WaterValue AddWaterValueByHouseID(long id, WaterValue value)
        {
            var wm = this.FindWmByHouseID(id);

            if (wm == null)
            {
                return null;
            }

            value.WaterMeter = wm;
            m_context.WaterValues.Add(value);
            m_context.SaveChanges();
            return value;

        }

        public WaterValue AddWaterValueBySerialNum(string serialNum, WaterValue value)
        {
            var wm = this.FindWmBySerialNum(serialNum);

            if (wm == null)
            {
                return null;
            }

            value.WaterMeter = wm;
            m_context.WaterValues.Add(value);
            m_context.SaveChanges();
            return value;

        }

        public IQueryable<WaterValue> GetAllWaterValues()
        {
            return m_context.WaterValues;
        }

        public IQueryable<WaterValue> GetAllWaterValuesByHouseId(long houseId)
        {
            return m_context.WaterValues.Where(x => x.WaterMeter.WM_id == houseId).OrderBy(x => x.ValueDate);
        }
   

    }
}