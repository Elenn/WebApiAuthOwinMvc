using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthMvc.Models;

namespace WebApiAuthMvc.Controllers
{
    
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult GetOrders()
        {
            return Ok(Order.CreateOrders());
        }

        //GetOrderByID 
        [Authorize]
        [Route("{id}")]
        public IHttpActionResult GetOrder(int id)
        { 
            return Ok(Order.GetOrderByID(id));
        }

        [Authorize]
        [HttpPost]
        [Route("")] 
        public IHttpActionResult PostData([FromBody]Order order) 
        { 
            return Ok(Order.AddOrder(order)); 
        }

        [Authorize]
        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]Order order)
        {
            return Ok(Order.UpdateOrder(order));
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id) 
        {
            return Ok(Order.DeleteOrder(id));
        }
    }

    #region Helpers 

    #endregion
}
