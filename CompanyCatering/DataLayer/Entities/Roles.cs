using System.ComponentModel.DataAnnotations;

namespace CompanyCatering.DataLayer.Entities
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool EmployeeSelected { get; set; }
        [Required]
        public bool CookSelected { get; set; }
        [Required]
        public string? RoleName { get; internal set; }

        public Roles(int id, bool employeeSelected, bool cookSelected, string roleName)
        {
            Id = id;
            EmployeeSelected = employeeSelected;
            CookSelected = cookSelected;
            RoleName = roleName;
        }

        public void OptionPressed()
        {
            if (EmployeeSelected = true)
            {
                RoleName = "Employee";
            }
            if (CookSelected = true)
            {
                RoleName = "Cook";
            }
        }
    }
}
