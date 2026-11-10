using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace Day5.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index(string search, string orderBy = "Id", bool dsc = false, int page = 1)
        {
            var data = from p in Data.SeedData.Products
                       join c in Data.SeedData.Categories on p.CategoryId equals c.CategoryId
                       select new Models.ProductViewModel
                       {
                           Id = p.ProductId,
                           ProductName = p.ProductName,
                           ProductPrice = p.ProductPrice,
                           CategoryName = c.CategoryName
                       };

            // 1. Search:
            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.Trim().ToLower();
                data = data.Where(x => x.ProductName.ToLower().Contains(s)
                                    || x.CategoryName.ToLower().Contains(s)
                                    || x.Id.ToString() == s);
            }

            // Sap xep
            string[] allowed = { "Id", "ProductName", "ProductPrice", "CategoryName" };
            if (!allowed.Contains(orderBy)) orderBy = "Id";
            var sorted = data.AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}");

            // phan trang
            int pageSize = 10;
            int total = sorted.Count();
            int pages = (int)Math.Ceiling((double)total / pageSize);
            if (pages == 0) pages = 1;
            if (page < 1) page = 1;
            if (page > pages) page = pages;

            var paged = sorted.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["Search"] = search ?? "";
            ViewData["OrderBy"] = orderBy;
            ViewData["Dsc"] = dsc;
            ViewData["Page"] = page;
            ViewData["Pages"] = pages;
            ViewData["Total"] = total;

            ViewData["IdSort"] = orderBy == "Id" && !dsc;
            ViewData["ProductNameSort"] = orderBy == "ProductName" && !dsc;
            ViewData["ProductPriceSort"] = orderBy == "ProductPrice" && !dsc;
            ViewData["CategoryNameSort"] = orderBy == "CategoryName" && !dsc;

            return View(paged);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var productViewModel = new Models.ProductViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CategoryName = category?.CategoryName ?? ""
            };
            return View(productViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            int newId = Data.SeedData.Products.Count == 0
                ? 1
                : Data.SeedData.Products.Max(p => p.ProductId) + 1;

            Data.SeedData.Products.Add(new Models.Product
            {
                ProductId = newId,
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                CategoryId = GetOrCreateCategoryId(model.CategoryName)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var model = new Models.ProductViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CategoryName = category?.CategoryName ?? ""
            };
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Models.ProductViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            product.ProductName = model.ProductName;
            product.ProductPrice = model.ProductPrice;
            product.CategoryId = GetOrCreateCategoryId(model.CategoryName);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var model = new Models.ProductViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CategoryName = category?.CategoryName ?? ""
            };
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null) Data.SeedData.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        private int GetOrCreateCategoryId(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return Data.SeedData.Categories[0].CategoryId;

            string name = categoryName.Trim();
            var category = Data.SeedData.Categories.FirstOrDefault(
                c => c.CategoryName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (category != null) return category.CategoryId;

            int newId = Data.SeedData.Categories.Count == 0
                ? 1
                : Data.SeedData.Categories.Max(c => c.CategoryId) + 1;
            Data.SeedData.Categories.Add(new Models.Category
            {
                CategoryId = newId,
                CategoryName = name
            });
            return newId;
        }
    }
}
