using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using WebAppFin.Models;



public class ShoppingListController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ShoppingListController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var shoppingLists = _db.ShoppingLists
                               .Where(list => list.UserName == userId)
                               .ToList();

        return View(shoppingLists);
    }


    public IActionResult CreateList()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateList(ShoppingLists shoppingList)
    {
        if (ModelState.IsValid)
        {
            // Check if a list with the same name exists for the current user
            if (!_db.ShoppingLists.Any(list => list.ApplicationUser.UserName == User.Identity.Name && list.ListName == shoppingList.ListName))
            {
                // Assign the current user to the shopping list
                shoppingList.ApplicationUser = await _userManager.FindByNameAsync(User.Identity.Name);

                _db.ShoppingLists.Add(shoppingList);
                _db.SaveChanges();
                return RedirectToAction("Index", "ShoppingList");
            }
            ModelState.AddModelError("ListName", "A list with the same name already exists for your username.");
        }
        return View(shoppingList);
    }

	public IActionResult DeleteList(int id)
	{
		var list = _db.ShoppingLists.FirstOrDefault(l => l.ListId == id );

		if (list == null)
		{
			return NotFound();
		}

		_db.ShoppingLists.Remove(list);
		_db.SaveChanges();

		return RedirectToAction("Index","ShoppingList");
	}


    public IActionResult Product(int listId)
    {
        return View();
    }


    public IActionResult AllProductNamesInCategory(int categoryId)
        {
            var productNames = _db.Products
                                  .Where(product => product.CategoryId == categoryId)
                                  .Select(product => product.ProductName)
                                  .Distinct()
                                  .ToList();

            return View(productNames);
        }

}

