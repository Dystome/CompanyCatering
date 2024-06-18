using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyCatering.Controllers
{
	public class EmployeeContoller : Controller
	{
		private readonly CompanyCateringDbContext _context;
		public EmployeeContoller(CompanyCateringDbContext context)
		{
			_context = context;
		}
		[Authorize(Roles = "Cook")]
		public IActionResult Index([FromQuery] string filterTerm)
		{
			var orders = _context.Employees
		   .Include(p => p.EmployeeOrders)
				.ThenInclude(p => p.FoodItems)
		  .Where(p => (p.IsDeleted == false || p.IsDeleted == null))
		  .OrderBy(p => p.FullName)
		  .ToList();
			if (!string.IsNullOrEmpty(filterTerm))
			{
				orders = orders.Where(p => p.FullName.Contains(filterTerm) ||
											 p.Email.Contains(filterTerm))
					.ToList();
			}
			return View(orders);
		}
		public IActionResult Index()
		{
			return View();
		}
		[Authorize(Roles = "Cook")]
		public IActionResult Details(int id)
		{
			var food = _context.Employees.Where(p => p.ID == id)
									  .FirstOrDefault();
			return View(food);
		}

		[Authorize(Roles = "Cook")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create([Bind("FullName,Email")] Employee employee)
		{
			var employeeExists = _context.Employees.Where(p => p.ID == employee.ID
															&& p.IsDeleted != true &&
															p.IsActive != false)
							   .FirstOrDefault();

			if (ModelState.IsValid)
			{
				_context.Employees.Add(employee);
				_context.SaveChanges();
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
			var employee = _context.Employees
				.Where(p => p.ID == id)
				.FirstOrDefault();
			return View(employee);
		}

		[HttpPost]
		public IActionResult Update([Bind("FullName, Email, ID")] Employee employee)
		{
			if (ModelState.IsValid)
			{
				_context.Employees.Update(employee);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return StatusCode(500, "Information is invalid");
			}
		}
		[Authorize(Roles = "Cook")]
		//Delete action pa view

		[HttpPost, ActionName("Delete")]
		public IActionResult Delete(int Id)
		{
			var employees = _context.Employees
				.Where(p => p.ID == Id)
				.FirstOrDefault();
			//Soft Delete
			employees.IsDeleted = true;
			//Hard Delete
			//_onlineLibraryDbContext.Clients.Remove(author);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Cook")]
		[HttpPost, ActionName("Deactivate")]
		public IActionResult Deactivate(int Id)
		{
			var employee = _context.Employees
				.Where(p => p.ID == Id)
				.FirstOrDefault();

			employee.IsActive = false;

			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Cook")]
		[HttpPost, ActionName("Activate")]
		public IActionResult Activate(int Id)
		{
			var employee = _context.Employees
				.Where(p => p.ID == Id)
				.FirstOrDefault();

			employee.IsActive = true;

			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
