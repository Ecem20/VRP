using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using WebAppFin.Models;

namespace WebAppFin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Categories.Any(c => c.CategoryId != category.CategoryId && c.CategoryName == category.CategoryName))

                    {
                        _db.Categories.Add(category);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("CategoryName", "Category name already exists.");
            }
            return View(category);
        }

        //GET 
        public IActionResult EditCategory(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _db.Categories.Find(id);
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Categories.Any(c => c.CategoryId != category.CategoryId && c.CategoryName == category.CategoryName))
                {
                    _db.Categories.Update(category);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("CategoryName", "Category name already exists.");
            }
            return View(category);
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _db.Categories.Find(id);
            if ( category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index","Admin");
        }



        //PRODUCT
        public IActionResult IndexProduct(int categoryId)
        {
            var category = _db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                //if (product.Icon != null && product.Icon.Length > 0)
                //{
                //    //product.Icon = new byte[product.Icon.Length];
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        product.Icon.CopyTo(memoryStream, (int)product.Icon.Length);

                //        product.Icon = memoryStream.ToArray();
                //    }
                //}

                if (!_db.Products.Any(p => p.ProductId != product.ProductId && p.ProductName == product.ProductName))

                    {
                        _db.Products.Add(product);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("ProductName", "Product name already exists.");
            }
            return View(product);
        }

        public IActionResult EditProduct(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {

            if (ModelState.IsValid)
            {
                if (!_db.Products.Any(p => p.ProductId != product.ProductId && p.ProductName == product.ProductName))
                {
                    _db.Products.Update(product);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("ProductName", "Product name already exists.");
            }
            return View(product);

        }

        public IActionResult DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index","Admin");
        }

    }

} 