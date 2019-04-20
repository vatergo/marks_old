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

        [HttpPost("[action]")]
        public IActionResult GetAllThings([FromHeader] Cookie cookie)
        {
            var things = context.Things.ToList();
            return Ok();
        }

        [HttpGet("{itemId}")]
        public IActionResult AddThing([FromRoute] string itemId)
        {
            if (string.IsNullOrEmpty(itemId) &&
                !itemId.All(char.IsDigit))
            {
                ModelState.AddModelError(nameof(itemId),
                    "ItemId should contain only digits.");
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);
            GetThingFromEbay(itemId);
            return Ok();
        }

        private async void GetThingFromEbay(string itemId)
        {
            var url = $"http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=IvanVate-marks-PRD-eea9be394-ffca6479&siteid=0&version=967&ItemID={itemId}";
            var xml = await new WebClient().DownloadStringTaskAsync(new Uri(url));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var thing = new Thing();
            var root = doc.DocumentElement;
            var item = GetXmlNode(root.ChildNodes, "Item");
            thing.ItemId = itemId;
            thing.Title = GetXmlNode(item.ChildNodes, "Title").InnerText;
            thing.Image = GetXmlNode(item.ChildNodes, "PictureURL").InnerText;
            thing.Cost = GetXmlNode(item.ChildNodes, "ConvertedCurrentPrice").InnerText +
            GetXmlNode(item.ChildNodes, "ConvertedCurrentPrice").Attributes[0].Value;
            //context.Things.Add(thing);
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
