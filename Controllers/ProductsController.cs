using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyPlayground.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }

        private readonly Product[] products =
        {
            new Product {Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1},
            new Product {Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M},
            new Product {Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            //if (product == null) return NotFound();
            return product;
        }
    }
}