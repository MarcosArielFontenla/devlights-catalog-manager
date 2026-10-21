using CatalogManager.Helpers;
using CatalogManager.Interfaces;
using CatalogManager.Models;
using CatalogManager.Presentation;
using CatalogManager.Services;

namespace CatalogManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var productService = new ProductService();

            foreach (var product in ProductHelper.CreateSampleProducts())
            {
                productService.Add(product);
            }

            var application = new CatalogConsoleApplication(productService);
            application.Run();
        }
    }
}