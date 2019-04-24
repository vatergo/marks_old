using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Thing
    {
        public Guid Id { get; set; }
        public string ItemId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }

    }
}
