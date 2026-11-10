using Day5.Models;

namespace Day5.Data
{
    public class SeedData
    {
        //gia lap csdl
        public static List<Category> Categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Điện thoại" },
            new Category { CategoryId = 2, CategoryName = "Tablet" }
        };
        public static List<Product> Products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Iphone 14", ProductPrice = 2000, CategoryId = 1 },
            new Product { ProductId = 2, ProductName = "Samsung Galaxy S23", ProductPrice = 1500, CategoryId = 1 },
            new Product { ProductId = 3, ProductName = "Macbook Pro M2", ProductPrice = 3000, CategoryId = 2 },
            new Product { ProductId = 4, ProductName = "iPad Pro", ProductPrice = 1200, CategoryId = 2 },
            new Product { ProductId = 5, ProductName = "Iphone 15", ProductPrice = 2500, CategoryId = 1 },
            new Product { ProductId = 6, ProductName = "Samsung Galaxy S24", ProductPrice = 1800, CategoryId = 1 },
            new Product { ProductId = 7, ProductName = "Xiaomi 14", ProductPrice = 900, CategoryId = 1 },
            new Product { ProductId = 8, ProductName = "Oppo Reno 11", ProductPrice = 700, CategoryId = 1 },
            new Product { ProductId = 9, ProductName = "Vivo V30", ProductPrice = 650, CategoryId = 1 },
            new Product { ProductId = 10, ProductName = "Iphone 13", ProductPrice = 1500, CategoryId = 1 },
            new Product { ProductId = 11, ProductName = "Samsung Galaxy A55", ProductPrice = 800, CategoryId = 1 },
            new Product { ProductId = 12, ProductName = "iPad Air", ProductPrice = 1400, CategoryId = 2 },
            new Product { ProductId = 13, ProductName = "iPad Mini", ProductPrice = 900, CategoryId = 2 },
            new Product { ProductId = 14, ProductName = "Samsung Galaxy Tab S9", ProductPrice = 1600, CategoryId = 2 },
            new Product { ProductId = 15, ProductName = "Lenovo Tab P12", ProductPrice = 750, CategoryId = 2 },
            new Product { ProductId = 16, ProductName = "Xiaomi Pad 6", ProductPrice = 850, CategoryId = 2 },
            new Product { ProductId = 17, ProductName = "Iphone 12", ProductPrice = 1100, CategoryId = 1 },
            new Product { ProductId = 18, ProductName = "Google Pixel 8", ProductPrice = 1300, CategoryId = 1 },
            new Product { ProductId = 19, ProductName = "Huawei MatePad", ProductPrice = 950, CategoryId = 2 },
            new Product { ProductId = 20, ProductName = "OnePlus 12", ProductPrice = 1700, CategoryId = 1 }
        };
    }
}
