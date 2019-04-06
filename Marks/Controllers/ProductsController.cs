using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Marks.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


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
            return Ok(context.Products.ToList());
        }

        // GET api/<controller>/5 Пока не разобрался как работают тут айдишники, сделал метод поиска по имени
        [HttpGet("{title}")]
        public ActionResult<ProductDto> GetProductByTitle(string title)
        {
            var products = context.Products.ToList();
            var product = products.Where(x => x.Title == title).FirstOrDefault();
            if (product != null)
            {
                var productDto = Mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }              
            return NotFound();
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
