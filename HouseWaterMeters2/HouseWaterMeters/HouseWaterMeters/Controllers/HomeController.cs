using HouseWaterMeters.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HouseWaterMeters.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
       
          return View();
        }
    //    [HttpGet]
    //    [Route("Home/Index")]
    //    public ActionResult Index()
    //    {
    //        var client = new HttpClient();
    //        var response = client.GetAsync("http://localhost:14681/api/House/GetAllHouses").Result;
    //        var houses = response.Content.ReadAsAsync<IEnumerable<House>>().Result;
    //        return View(houses);
    //    }

    //    [HttpPost]
    //    [Route("Home/Add")]
    //    public ActionResult Add(House item)
    //    {
    //        if (ModelState.IsValid)
    //        {

    //            var client = new HttpClient();
    //            StringContent queryString = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
    //            var response = client.PostAsync("http://localhost:14681/api/House/AddHouse", queryString).Result;
    //            return RedirectToAction("Index");
    //        }
    //        else return View("Index");
    //    }

    //    [HttpPost]
    //    [Route("Home/Update")]
    //    public ActionResult Update(House item)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var client = new HttpClient();
    //            StringContent queryString = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
    //            var response = client.PutAsync("http://localhost:14681/api/House/UpdateHouse", queryString).Result;
    //            return RedirectToAction("Index");
    //        }
    //        else return View("Index");
    //    }

    //    [HttpGet]
    //    [Route("Home/Remove")]
    //    public ActionResult Remove(long id)
    //    {
    //        if (ModelState.IsValid)
    //        {

    //            var client = new HttpClient();
    //            var response = client.DeleteAsync("http://localhost:14681/api/House/DeleteHouse/" + id).Result;
    //            return RedirectToAction("Index");
    //        }
    //        else return View("Index");
    //    }

    }


}

