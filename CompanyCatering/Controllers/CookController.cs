using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyCatering.Controllers
{
    public class CookController : Controller
    {
        private readonly CompanyCateringDbContext companyCateringDbContext;
        public CookController(CompanyCateringDbContext companyCateringDbContext)
        {
            this.companyCateringDbContext = companyCateringDbContext;
        }

        public IActionResult Index([FromQuery] string filterTerm)
        {
            var items = companyCateringDbContext.FoodItems
                .Include(p => p.Name)
                .Where(p => p.IsDeleted == false || p.IsDeleted == null)
                .OrderBy(p => p.Name);
            if (!string.IsNullOrEmpty(filterTerm))
            {
                items = (IOrderedQueryable<DataLayer.Entities.FoodItems>)items.Where(p => p.Name.Contains(filterTerm) ||
                                             p.Description.Contains(filterTerm))
                    .ToList();
            }
            return View(items);
        }
        public IActionResult Details(int id)
        {
            var items = companyCateringDbContext.FoodItems/*.Include(p => p.Author)*/
                                      .Where(p => p.Id == id)
                                      .FirstOrDefault();
            return View(items);
        }
        [Authorize(Roles = "Cook")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("FullName,Description")] FoodItems items)
        {
            items.PhotoPath = "path ";

            if (ModelState.IsValid)
            {
                items.DateAdded = DateTime.Now;
                companyCateringDbContext.FoodItems.Add(items);
                companyCateringDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                return StatusCode(500, "Information is invalid");
            }

        }
        [Authorize(Roles = "Cook")]
        public IActionResult Update([FromRoute] int id)
        {
            var items = companyCateringDbContext.FoodItems
                .Where(p => p.Id == id)
                .FirstOrDefault();
            return View(items);
        }
        [HttpPost]
        public IActionResult Update([Bind("FullName, Description, Id")] FoodItems items)
        {
            if (ModelState.IsValid)
            {
                items.DateUpdated = DateTime.Now;
                companyCateringDbContext.FoodItems.Update(items);
                companyCateringDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return StatusCode(500, "Information is invalid");
            }
        }
        [Authorize(Roles = "Cook")]

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            var items = companyCateringDbContext.FoodItems
                .Where(p => p.Id == Id)
                .FirstOrDefault();
            //Soft Delete
            items.IsDeleted = true;
            //Hard Delete
            //companyCateringDbContext.FoodItems.Remove(author);
            companyCateringDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
