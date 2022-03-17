using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Model
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
    }

    public class Products
    {
        public Product[]? Stock { get; set; }
    }
}
