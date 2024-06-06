using Microsoft.AspNetCore.Identity;

namespace CompanyCatering.Models
{

    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string IsAdmin { get; set; }
        public string Role {  get; set; }
        public string Phone { get; set; }
    }

}
