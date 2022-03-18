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

        public void Set(Products products)
        {
            using FileStream fs = new(Filepath, FileMode.Create);

            JsonSerializer.Serialize(fs, products);
        }

        public void Add(Product product)
        {
            Products prods = new()
            {
                Stock = Get()
            };

            if (prods.Stock != null)
            {
                List<Product> prodList = new List<Product>(prods.Stock);
                prodList.Add(product);
                prods.Stock = prodList.ToArray();
            }
            else
            {
                prods.Stock = new Product[]{ product };
            }

            Set(prods);
        }

        public void Remove(int index)
        {
            List<Product> prodList = new(Get());
            prodList.RemoveAt(index);
            Set(new Products() { Stock = prodList.ToArray() });
        }
    }
}
