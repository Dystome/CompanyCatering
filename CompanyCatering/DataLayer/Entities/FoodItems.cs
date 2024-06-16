using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompanyCatering.DataLayer.Entities
{
    public class FoodItems
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey ("FoodCategory")]
        public int FoodCatergoryId { get; set; }
        [Required]
        public double Price { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string? PhotoPath { get; set; }
        public bool IsDeleted = false;



    }
}
