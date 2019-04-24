using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using DataLayer;
using DataLayer.Entities;
using Marks.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marks.Controllers
{
    [Route("api/[controller]")]
    public class ThingsController : Controller
    {
        private EFDBContext context;
        public ThingsController(EFDBContext context)
        {
            this.context = context;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllThings([FromHeader(Name = "Cookie")] string userName)
        {
            var name = userName.Split('=')[1];
            var things = context.Things.Where(x => x.UserName == name).ToList();
            return Ok(things);
        }

        [HttpGet("{itemId}")]
        public IActionResult AddThing([FromRoute] string itemId, [FromHeader(Name = "Cookie")] string userName)
        {
            var name = userName.Split('=')[1];
            if (string.IsNullOrEmpty(itemId) &&
                !itemId.All(char.IsDigit))
            {
                ModelState.AddModelError(nameof(itemId),
                    "ItemId should contain only digits.");
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            var thing = GetThingFromEbay(itemId);
            if (thing == null) return BadRequest();
            thing.UserName = name;
            var createdProduct = context.Things.Add(thing);
            context.SaveChanges();
            return CreatedAtRoute(
                new { userId = createdProduct.Entity.Id },
                createdProduct.Entity.Id);
        }

        private Thing GetThingFromEbay(string itemId)
        {
            var url = $"http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=IvanVate-marks-PRD-eea9be394-ffca6479&siteid=0&version=967&ItemID={itemId}";
            var xml = new WebClient().DownloadString(new Uri(url));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var thing = new Thing();
            var root = doc.DocumentElement;
            var item = GetXmlNode(root.ChildNodes, "Item");
            if (item == null) return null;
            thing.ItemId = itemId;
            thing.Title = GetXmlNode(item.ChildNodes, "Title").InnerText;
            thing.Image = GetXmlNode(item.ChildNodes, "PictureURL").InnerText;
            thing.Cost = GetXmlNode(item.ChildNodes, "ConvertedCurrentPrice").InnerText +
            GetXmlNode(item.ChildNodes, "ConvertedCurrentPrice").Attributes[0].Value;
            return thing;
        }

        private XmlNode GetXmlNode(XmlNodeList list, string name)
        {
            XmlNode result = null;
            for(var i = 0; i<list.Count; i++)
            {
                if (list[i].Name == name)
                {
                    result = list[i];
                    break;
                }    
            }
            return result;
        }
    }
}
