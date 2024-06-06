using CompanyCatering.DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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

        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodItems> FoodItems { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles {  get; set; }
    }
}
