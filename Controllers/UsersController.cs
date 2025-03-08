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

namespace AdminMVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "gG57iPueijVJNLppwAbDQ99aTzHJI9ozZAbMAKlc",
            BasePath = "https://taxiapp-f3caa-default-rtdb.firebaseio.com/"

        };
        IFirebaseClient client;
        // GET: Client
       
        public ActionResult Users()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("users");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Users>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Users>(((JProperty)item).Value.ToString()));
            }
            return View(list);
        }
        
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(Users users)
        //{
            
        //        AddUserToFirebase(users);
        //        ModelState.AddModelError(string.Empty, "Added Successfully!");
            
            
        //    return View();
        //}

        //private void AddUserToFirebase(Users users)
        //{
        //    client = new FireSharp.FirebaseClient(config);
        //    var data = users;
        //    PushResponse response = client.Push("users/", data);
        //    data.id = response.Result.name;
        //    SetResponse setResponse = client.Set("users/" + data.id, data);
        //}
        [HttpGet]
        public ActionResult Details(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("users/" + id);
            Users data = JsonConvert.DeserializeObject<Users>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("users/" + id);
            Users data = JsonConvert.DeserializeObject<Users>(response.Body);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Users users)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("users/" + users.id, users);
            return RedirectToAction("users");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("users/" + id);
            return RedirectToAction("users");
        }
    }
}