using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiAuthMvc.Models;
using WebApiAuthMvc.Clients;
using Newtonsoft.Json; 
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Web.Script.Serialization;

namespace WebApiAuthMvc.Controllers
{
    [Authorize]
    public class OrderListController : Controller
    {  
        // GET: OrderList 
        public ActionResult Index()
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            List<Order> orderList = new List<Order>();  

            var resultContent = GenericClient.GetEvent("http://localhost:57359/api/orders", "", token);
            if (resultContent != "")
                orderList = JsonConvert.DeserializeObject<List<Order>>(resultContent);

            return View(orderList);
        }

        // GET:   
        public ActionResult Create() 
        { 
            return View();
        }

        // POST:  
        [HttpPost]
        public ActionResult Create(Order model) 
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            var inputJson = new JavaScriptSerializer().Serialize(model);

            var resultContent = GenericClient.PostEvent("http://localhost:57359/api/orders", inputJson, "POST", token);

            return RedirectToAction("Index", "OrderList");
        }

        // GET:   
        public ActionResult Edit(int id = 0) 
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            Order order = null;

            string url = "http://localhost:57359/api/orders/"; 
            var urlWithParam = string.Format("{0}{1}", url, id);  

            var resultContent = GenericClient.GetEvent(urlWithParam, "", token);
            if (resultContent != "")
                order = JsonConvert.DeserializeObject<Order>(resultContent);

            return View(order);
        } 
         
        // POST:  
        [HttpPost]
        public ActionResult Edit(Order model) 
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            var inputJson = new JavaScriptSerializer().Serialize(model);

            var resultContent = GenericClient.PostEvent("http://localhost:57359/api/orders", inputJson, "PUT", token);  

            return RedirectToAction("Index", "OrderList");
        }

        // GET:   
        public ActionResult Delete(int id = 0)   
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            Order order = null;

            string url = "http://localhost:57359/api/orders/";
            var urlWithParam = string.Format("{0}{1}", url, id);

            var resultContent = GenericClient.GetEvent(urlWithParam, "", token);
            if (resultContent != "")
                order = JsonConvert.DeserializeObject<Order>(resultContent);

            return View("Details", order); 
        }

       

        [HttpPost, ActionName("Delete")] 
        public ActionResult DeleteConfirmed(int id)
        {
            UserSession userSession = new UserSession();
            string token = userSession.BearerToken;

            string url = "http://localhost:57359/api/orders/";
            var urlWithParam = string.Format("{0}{1}", url, id);

            var resultContent = GenericClient.PostEvent(urlWithParam, "", "DELETE", token);

            return RedirectToAction("Index");
        }
    }
}