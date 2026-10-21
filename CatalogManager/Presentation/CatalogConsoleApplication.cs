using System;
using System.Collections.Generic;
using CatalogManager.Interfaces;
using CatalogManager.Models;

namespace CatalogManager.Presentation
{
    public class CatalogConsoleApplication
    {
        private readonly IProductService _productService;

        public CatalogConsoleApplication(IProductService productService)
        {
            if (productService == null)
            {
                throw new ArgumentNullException(nameof(productService));
            }

            _productService = productService;
        }

        public void Run()
        {
            Console.WriteLine("=== All products ===");
            PrintProducts(_productService.GetAll());

            int productId = 2;
            Console.WriteLine($"\n=== Product with ID {productId} ===");
            Product product = _productService.GetById(productId);

            if (product != null)
            {
                PrintProduct(product);
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            string category = "Electronics";
            Console.WriteLine($"\n=== Products in category: {category} ===");
            PrintProducts(_productService.FindByCategory(category));
        }

        private static void PrintProducts(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in products)
            {
                PrintProduct(product);
            }
        }

        private static void PrintProduct(Product product)
        {
            Console.WriteLine(
                $"ID: {product.Id} | Name: {product.Name} | Price: {product.Price:F2} | " +
                $"Category: {product.Category} | Active: {product.IsActive}");
        }
    }
}
