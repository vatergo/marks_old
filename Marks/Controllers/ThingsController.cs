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
            var url = @"http://open.api.ebay.com/shopping?callname=GetSingleItem&responseencoding=XML&appid=IvanVate-marks-PRD-eea9be394-ffca6479&siteid=0&version=967&ItemID={itemId}";
            var xml = await new WebClient().DownloadStringTaskAsync(new Uri(url));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.DocumentElement.ChildNodes;
            XmlNode item;
            for (var i = 0; i < root.Count; i++)
            {
                if (root[i].Name == "Item")
                {
                    item = root[i];
                    break;
                }
            }
            //var title = item.SelectSingleNode("ItemID").Value;
            //var imgUrl = item.SelectSingleNode("PictureURL").Value;
            //var cost = item.SelectSingleNode("ConvertedCurrentPrice").Value +
            //item.SelectSingleNode("ConvertedCurrentPrice").Attributes["currencyID"];
            var vg = 0;
        }
    }
}
