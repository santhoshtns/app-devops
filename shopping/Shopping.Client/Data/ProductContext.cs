using Shopping.Client.Models;
using System.Collections.Generic;

namespace Shopping.Client.Data
{
    public static class ProductContext
    {
        public static readonly List<Product> Products = new List<Product>()
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
