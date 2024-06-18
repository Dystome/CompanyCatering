using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyCatering.Models;

namespace CompanyCatering.Controllers
{
	public class FoodOrderContoller : Controller
	{
		private readonly CompanyCateringDbContext _context;
		public FoodOrderContoller(CompanyCateringDbContext context)
		{
			_context = context;
		}
		[Authorize(Roles = "Cook")]
		public IActionResult Index()
		{

			var orderFood = _context.EmployeeOrders
				.Include(p => p.Employee)
				.Include(p => p.FoodItems)
				.Where(p => p.IsDelete != false)
				.OrderBy(p => p.FoodItems.Name)
				.ToList();

			return View(orderFood);
		}
		[Authorize(Roles = "Cook")]
		public IActionResult Create()
		{
			var foodOrderModel = new FoodOrderModel();
			foodOrderModel.Employees = _context.Employees.Where(p => p.IsActive != false &&
			p.IsDeleted != true)
													.ToList();
			foodOrderModel.FoodItems = _context.FoodItems.Where(p => p.IsDeleted != true)
												.ToList();
			return View(foodOrderModel);
		}

		[HttpPost, ActionName("Create")]
		public IActionResult Create([Bind("ClientID, BookID")] EmployeeOrder employeeOrder)
		{
			if (ModelState.IsValid)
			{
				_context.EmployeeOrders.Add(employeeOrder);
				_context.SaveChanges();
				return RedirectToAction("Index", "Books");
			}
			else
			{
				return StatusCode(500, "Information is invalid");
			}
		}
	}
}
