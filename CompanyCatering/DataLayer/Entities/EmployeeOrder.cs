using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyCatering.DataLayer.Entities
{
	public class EmployeeOrder
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Employee")]
		public int ID { get; set; }
		public virtual Employee? Employee { get; set; }
		[ForeignKey("FoodItems")]
		public int FoodID { get; set; }
		public virtual FoodItems? FoodItems { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public bool? IsDelete { get; set; }
		public EmployeeOrder()
		{
			CreatedDate = DateTime.Now;
			Status = "New";
		}
	}
}
