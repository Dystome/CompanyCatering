using Microsoft.AspNetCore.Mvc;
using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.Controllers
{
	public class EmployeeOrderController : Controller
	{
		private readonly CompanyCateringDbContext _context;
		public EmployeeOrderController(CompanyCateringDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Create()
		{
			return View();
		}


	}
}

