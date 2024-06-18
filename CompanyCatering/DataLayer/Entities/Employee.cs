using System.ComponentModel.DataAnnotations;

namespace CompanyCatering.DataLayer.Entities
{
	public class Employee
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string FullName { get; set; }
		public bool? IsDeleted { get; set; }
		public bool? IsActive { get; set; }
		public string? PhotoPath { get; set; }

		public virtual List<EmployeeOrder> EmployeeOrders { get; set; }

	}
}
