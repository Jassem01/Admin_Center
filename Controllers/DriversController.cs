using TLE0.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace hhhh.Controllers
{
    [Authorize]
    public class DriversController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "gG57iPueijVJNLppwAbDQ99aTzHJI9ozZAbMAKlc",
            BasePath = "https://taxiapp-f3caa-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        
        public ActionResult Drivers()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("drivers/");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Drivers>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Drivers>(((JProperty)item).Value.ToString()));
            }
            return View(list);
        }
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(Drivers drivers)
        //{

        //    AddDriverToFirebase(drivers);
        //    ModelState.AddModelError(string.Empty, "Added Successfully!");



        //    return View();
        //}

        //private void AddDriverToFirebase(Drivers drivers)
        //{
        //    client = new FireSharp.FirebaseClient(config);
        //    var data = drivers;
        //    PushResponse response = client.Push("drivers/", data);
        //    data.id = response.Result.name;
        //    SetResponse setResponse = client.Set("drivers/" + data.id, data);
        //}
        [HttpGet]
        public ActionResult Details(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("drivers/" + id);
            Drivers data = JsonConvert.DeserializeObject<Drivers>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("drivers/" + id);
            Drivers data = JsonConvert.DeserializeObject<Drivers>(response.Body);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Drivers drivers)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("drivers/" + drivers.id , drivers);
            return RedirectToAction("drivers");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("drivers/" + id);
            return RedirectToAction("drivers");
        }
    }
}
