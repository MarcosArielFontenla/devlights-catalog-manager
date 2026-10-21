using System.Collections.Generic;
using CatalogManager.Models;

namespace CatalogManager.Helpers
{
    public static class ProductHelper
    {
        public static List<Product> CreateSampleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200.00m,
                    Category = "Electronics",
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 25.50m,
                    Category = "Electronics",
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "Notebook",
                    Price = 4.75m,
                    Category = "Stationery",
                    IsActive = true
                },
                new Product
                {
                    Id = 4,
                    Name = "Desk chair",
                    Price = 150.00m,
                    Category = "Furniture",
                    IsActive = false
                }
            };
        }
    }
}
