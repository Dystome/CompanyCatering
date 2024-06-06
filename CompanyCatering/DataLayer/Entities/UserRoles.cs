using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyCatering.DataLayer.Entities
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey ("Users")]  
        public int UserId { get; set; }
        [ForeignKey ("Roles")]
        public int RoleId { get; set; }
    }
}
