using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if (context.Products.Any())
            {
                context.Products.Add(new Product() { Title = "First product" });
                context.SaveChanges();
            }
        }
    }
}
