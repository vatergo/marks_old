using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
