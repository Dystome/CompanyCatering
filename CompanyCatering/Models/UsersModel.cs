using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.Models
{
    public class UsersModel:Users
    {
           public virtual List<string> Roles { get; set; }

    }
}
