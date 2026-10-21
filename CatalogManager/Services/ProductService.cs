using System;
using System.Collections.Generic;
using System.Linq;
using CatalogManager.Interfaces;
using CatalogManager.Models;

namespace CatalogManager.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new List<Product>();

        public void Add(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            _products.Add(product);
        }

        public List<Product> GetAll()
        {
            return _products.ToList();
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> FindByCategory(string category)
        {
            return _products
                .Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}