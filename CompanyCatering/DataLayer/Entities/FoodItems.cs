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

        private bool IsDeleted = false;



    }
}
