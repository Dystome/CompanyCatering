using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CompanyCatering.DataLayer.Entities
{
    public class Users : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }

        public DateTime LastLogin { get; set; }
        public bool Role { get; set; }

        public string? isAdmin { get; set; }
        public bool? isDeleted { get; set; }


    }
}
