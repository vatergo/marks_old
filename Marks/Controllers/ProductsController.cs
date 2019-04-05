using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marks.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private EFDBContext context;

        public ProductsController(EFDBContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            return Ok();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string GetProductById(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
