using Microsoft.AspNetCore.Identity;

namespace CompanyCatering.Models
{
    public class UserRoleDisplayModel
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public IdentityRole Roles { get; set; };
    }
}
