using StockManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Services
{
    public class JsonStockManager : IStockManager
    {
        public string? Filepath { get; init; } = "Stock.json";

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public Product[] Get()
        {
            throw new NotImplementedException();
        }
    }
}
