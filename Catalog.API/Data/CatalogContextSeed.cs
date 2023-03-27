using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
                productCollection.InsertManyAsync(GetMyProducts());
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "796n6766bkfYUKFFK76",
                    Name = "IPHONE 5",
                    Description = "Description1",
                    Image = "image-pd.png",
                    Price = 10.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "796n6766bkfYUKFFK77",
                    Name = "IPHONE 6",
                    Description = "Description2",
                    Image = "image-p2d.png",
                    Price = 11M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "796n6766bkfYUKFFK76",
                    Name = "IPHONE 8",
                    Description = "Description3",
                    Image = "image-pd3.png",
                    Price = 13M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "796n6766bkfYUKFFK76",
                    Name = "TABLET 1",
                    Description = "Description10",
                    Image = "image-pd12.png",
                    Price = 100M,
                    Category = "TABLET"
                },
                new Product()
                {
                    Id = "796n6766bkfYUKFFK76",
                    Name = "TABLET 2",
                    Description = "Description10",
                    Image = "image-pd13.png",
                    Price = 101.00M,
                    Category = "TABLET"
                }
            };
        }
    }
}
