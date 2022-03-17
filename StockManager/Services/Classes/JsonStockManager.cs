using StockManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockManager.Services
{
    public class JsonStockManager : IStockManager
    {
        private string? _filepath;
        public string? Filepath 
        {
            get => _filepath;
            init
            {
                _filepath = value;
                using FileStream fs = new(_filepath, FileMode.OpenOrCreate);
            }
        }

        public JsonStockManager()
        {
            Filepath = "Stock.json";
        }

        public void Add(Product product)
        {
            Products prods = new()
            {
                Stock = Get()
            };

            if (prods.Stock != null)
            {
                List<Product> products = new List<Product>(prods.Stock);
                products.Add(product);
                prods.Stock = products.ToArray();

                using FileStream fs = new(Filepath, FileMode.Create);

                JsonSerializer.Serialize(fs, prods);
            }
            else
            {
                using FileStream fs = new(Filepath, FileMode.OpenOrCreate);

                prods.Stock = new Product[]{ product };

                JsonSerializer.Serialize(fs, prods);
            }
        }

        public Product[]? Get()
        {
            using FileStream fs = new(Filepath, FileMode.OpenOrCreate);

            Product[]? products = null;

            try
            {
                products = JsonSerializer.Deserialize<Products>(fs).Stock;
            }
            catch (JsonException) { }

            return products;
        }
    }
}
