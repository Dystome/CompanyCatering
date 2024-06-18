using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.Models
{
	public class FoodOrderModel
	{
		public int EmployeeID { get; set; }
		public int FoodID { get; set; }
		public List<Employee> Employees { get; set; }
		public List<FoodItems> FoodItems { get; set; }
	}
}
