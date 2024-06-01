using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyCatering.DataLayer.DBContext
{
    public class CompanyCateringDbContext : IdentityDbContext
    {
        public CompanyCateringDbContext()
        {
        }

        public CompanyCateringDbContext(DbContextOptions<CompanyCateringDbContext> options) : base(options)
        {
        }   
    }
}
