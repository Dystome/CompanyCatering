namespace CompanyCatering.Models
{
    public class AccountRegisterModel
    {
        internal string RoleName;

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string IdNumber { get; set; }
        
        public string Password { get; set; }
        
        public DateTime RegistrationDate { get; set; }

        public bool EmployeeSelected { get; set; }
        public bool CookSelected { get; set; }

    }
}
