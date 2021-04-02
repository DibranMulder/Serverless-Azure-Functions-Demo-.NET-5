using DataModel.Models;
using System.Collections.Generic;

namespace ProductFunctions
{
    public static class ProductStore
    {
        public static readonly List<Product> Products = new List<Product>()
        {
            new Product() { Name = "Book", Price = 9.99, Manufacturer = "O'Reilly" },
            new Product() { Name = "Car", Price = 45000, Manufacturer = "Tesla" },
            new Product() { Name = "Starship", Price = 9999999999, Manufacturer = "SpaceX" },
        };
    }
}
