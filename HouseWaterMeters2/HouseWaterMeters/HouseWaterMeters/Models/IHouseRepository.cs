using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HouseWaterMeters.Models
{
    public interface IHouseRepository
    {
        //create new house
        House Add(House house);

        //get all houses
        IQueryable<House> GetAll();

        Task<IEnumerable<House>> GetAllAsync();

        //get all water meters
        IQueryable<WaterMeter> GetAllWaterMeters();

        //find house  
        House Find(long id);

        WaterMeter FindWmByHouseID(long id);

        WaterMeter FindWmBySerialNum(string serialNum);

        House Delete(long id);

        House Update(House house);

        WaterMeter AddWaterMeterByHouseID(long houseID, WaterMeter newWaterMeter);

        House GetMaxConsumedHouse(DateTime startDate, DateTime endDate);

        House GetMinConsumedHouse(DateTime startDate, DateTime endDate);

        WaterValue AddWaterValueByHouseID(long id, WaterValue value);

        WaterValue AddWaterValueBySerialNum(string serial, WaterValue value);

        IQueryable<WaterValue> GetAllWaterValues();

        IQueryable<WaterValue> GetAllWaterValuesByHouseId(long houseId);
    }
}