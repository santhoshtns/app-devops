using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shopping.API.Models;
using System.Collections.Generic;

namespace Shopping.API.Data
{
    public class ProductContext
    {
        public ProductContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);
            SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }

        private static void SeedData(IMongoCollection<Product> productsCollection)
        {
            bool existProduct = productsCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productsCollection.InsertMany(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "IPhone X",
                    Description = "IPhone X Description",
                    ImageFile = "product1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Samsung S20",
                    Description = "Samsung S20 Description",
                    ImageFile = "product2.png",
                    Price = 1050.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Samsung S10",
                    Description = "Samsung S10 Description",
                    ImageFile = "product3.png",
                    Price = 900.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Huawei P20",
                    Description = "Huawei P20 Description",
                    ImageFile = "product4.png",
                    Price = 700.00M,
                    Category = "Smart Phone"
                }
            };
        }
    }
}
