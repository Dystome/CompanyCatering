using System.ComponentModel.DataAnnotations;

namespace CompanyCatering.DataLayer.Entities
{
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
