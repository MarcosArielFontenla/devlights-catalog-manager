using System.Collections.Generic;
using CatalogManager.Models;

namespace CatalogManager.Interfaces
{
    public interface IProductService
    {
        void Add(Product product);
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> FindByCategory(string category);
    }
}
