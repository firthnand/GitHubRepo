using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HouseWaterMeters.Models;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace HouseWaterMeters.Controllers
{
    public class HouseController : ApiController
    {
        private readonly IHouseRepository m_houseRepository;

        public HouseController(IHouseRepository houseRepository)
        {
            this.m_houseRepository = houseRepository;
        }

        // GET: api/House/GetAllHouses
        /// <summary>
        /// Get all houses.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllHousesAsync")]
        //[ResponseType(typeof(IEnumerable<House>))]
        public async Task<IEnumerable<House>> GetAllHousesAsync()
        {
            var list =  await m_houseRepository.GetAllAsync();
            
            if (list == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return list;
        }

        [HttpGet]
        [ActionName("GetAllHouses")]
        public IEnumerable<House> GetAllHouses()
        {
            return m_houseRepository.GetAll();
        }

        // GET: api/House/GetHouse/5
        /// <summary>
        /// Get house by ID.
        /// </summary>
        /// <param name="id"> house id</param>
        /// <returns>House with specified id </returns>
        [HttpGet]
        [ActionName("GetHouse")]
        public House GetHouse(long id)
        {
            var house = m_houseRepository.Find(id);
            if (house == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return house;
        }

        // POST: api/House/AddHouse
        /// <summary>
        /// Add a new house
        /// </summary>
        /// <param name="house">House to add.</param>
        /// <return
        [AcceptVerbs("POST")]
        [ActionName("AddHouse")]
        public HttpResponseMessage AddHouse([FromBody]House house)
        {
            if (house == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var response = Request.CreateResponse<House>(HttpStatusCode.Created, house);
            try
            {
                m_houseRepository.Add(house);
            }
            catch (DbEntityValidationException ex)
            {
                response.Content =  new StringContent(GetErrorLog(ex));
            }
            response.Headers.Location = GetHouseLocation(house.HouseId);
            return response;
        }

        private Uri GetHouseLocation(long houseId)
        {
            var controller = this.Request.GetRouteData().Values["controller"];
            return new Uri(this.Url.Link("DefaultApi", new { controller = controller, id = houseId }));
        }

        private string GetErrorLog(DbEntityValidationException ex)
        {
            string errorLog = "";
            foreach (var entityValidationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in entityValidationErrors.ValidationErrors)
                {
                    errorLog += ("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                }
            }
            return errorLog;
        }

        // PUT: api/House/UpdateHouse/5
        /// <summary>
        /// Update an existing house.
        /// </summary>
        /// <param name="house">House update.</param>
        /// <returns>the updated house.</returns>
        [AcceptVerbs("PUT")]
        [ActionName("UpdateHouse")]
        public HttpResponseMessage UpdateHouse([FromBody]House house)
        {
            if (house == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var response = Request.CreateResponse<House>(HttpStatusCode.Created, house);
            try
            {
                var updated = m_houseRepository.Update(house);
                if (updated == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
            catch (DbEntityValidationException ex)
            {
                response.Content = new StringContent(GetErrorLog(ex));
            }
            response.Headers.Location = GetHouseLocation(house.HouseId);
            return response;
        }

        // DELETE: api/House/DeleteHouse/5
        /// <summary>
        /// Delete a house.
        /// </summary>
        /// <param name="id">House ID.</param>
        /// <returns>The deleted house.</returns>
        [AcceptVerbs("DELETE")]
        [ActionName("DeleteHouse")]
        public House DeleteHouse(int id)
        {
            var deleted = this.m_houseRepository.Find(id);
            if (deleted == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            this.m_houseRepository.Delete(id);
            return deleted;
        }

        //AddWaterMeterByHouseID
        // PUT: api/House/AddWaterMeterByHouseID/5
        /// <summary>
        /// Update an existing house.
        /// </summary>
        /// <param name="house">House update.</param>
        /// <returns>the updated house.</returns>
        [AcceptVerbs("POST")]
        [ActionName("AddWaterMeterByHouseID")]
        public HttpResponseMessage AddWaterMeterByHouseID(long houseId, [FromBody]WaterMeter wm)
        {
            if (wm == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = Request.CreateResponse<WaterMeter>(HttpStatusCode.Created, wm);

            try
            {
                var updated = m_houseRepository.AddWaterMeterByHouseID(houseId, wm);
                if (updated == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
            catch (DbEntityValidationException ex)
            {
                response.Content = new StringContent(GetErrorLog(ex));
            }
            response.Headers.Location = GetHouseLocation(wm.WM_id);
            return response;
        }

        // GET: api/House/GetAllWaterMeters
        /// <summary>
        /// Get all water meters.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllWaterMeters")]
        public IEnumerable<WaterMeter> GetAllWaterMeters()
        {
            return m_houseRepository.GetAllWaterMeters();
        }

        // GET: api/House/WaterMeters
        /// <summary>
        /// Get all water meters.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddWaterValueByHouseID")]
        public HttpResponseMessage AddWaterValueByHouseID(long houseId, [FromBody]WaterValue wv)
        {
            if (wv == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = Request.CreateResponse<WaterValue>(HttpStatusCode.Created, wv);

            try
            {
                var updated = m_houseRepository.AddWaterValueByHouseID(houseId, wv);
                if (updated == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
            catch (DbEntityValidationException ex)
            {
                response.Content = new StringContent(GetErrorLog(ex));
            }

            response.Headers.Location = GetHouseLocation(houseId);
            return response;
        }

        // GET: api/House/WaterMeters
        /// <summary>
        /// Get all water meters.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddWaterValueBySerialNumber")]
        public HttpResponseMessage AddWaterValueBySerialNumber(string serialNumber, [FromBody]WaterValue wv)
        {
            if (wv == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var response = Request.CreateResponse<WaterValue>(HttpStatusCode.Created, wv);

            try
            {
                var updated = m_houseRepository.AddWaterValueBySerialNum(serialNumber, wv);
                if (updated == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
            }
            catch (DbEntityValidationException ex)
            {
                response.Content = new StringContent(GetErrorLog(ex));
            }

            response.Headers.Location = GetHouseLocation(m_houseRepository.FindWmBySerialNum(serialNumber).WMHouse.HouseId);
            return response;
        }

        [HttpGet]
        [ActionName("GetAllWaterValues")]
        public IEnumerable<WaterValue> GetAllWaterValues()
        {
            return m_houseRepository.GetAllWaterValues();
        }

        [HttpGet]
        [ActionName("GetAllWaterValuesByHouseId")]
        public IEnumerable<WaterValue> GetAllWaterValuesByHouseId(long houseId)
        {
            return m_houseRepository.GetAllWaterValuesByHouseId(houseId);
        }

        // GET: api/House/GetMaxConsumedHouse?startDate={startDate}&endDate={endDate}
        /// <summary>
        /// Get maximal consumpted house.
        /// </summary>
        /// <param name="startDate"> start of interval </param>
        /// <param name="endDate"> end of interval </param>
        /// <returns>House</returns>
        [HttpGet]
        [ActionName("GetMaxConsumedHouse")]
        public House GetMaxConsumedHouse(DateTime startDate, DateTime endDate)
        {
            var house = m_houseRepository.GetMaxConsumedHouse(startDate, endDate);
            if (house == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return house;
        }


        // GET: api/House/GetMaxConsumedHouse?startDate={startDate}&endDate={endDate}
        /// <summary>
        /// Get maximal consumpted house.
        /// </summary>
        /// <param name="startDate"> start of interval </param>
        /// <param name="endDate"> end of interval </param>
        /// <returns>House</returns>
        [HttpGet]
        [ActionName("GetMinConsumedHouse")]
        public House GetMinConsumedHouse(DateTime startDate, DateTime endDate)
        {
            var house = m_houseRepository.GetMinConsumedHouse(startDate, endDate);
            if (house == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return house;
        }


    }
}
