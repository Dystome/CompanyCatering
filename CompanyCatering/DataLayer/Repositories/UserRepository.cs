using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.DataLayer.Repositories
{
	public class UserRepository:IUserRepository
	{
		private readonly CompanyCateringDbContext companyCateringDbContext;
		public UserRepository()
		{
			companyCateringDbContext = new CompanyCateringDbContext();
		}

		public List<Users> GetAllUsers()
		{
			var authors = companyCateringDbContext.Users.ToList();
			return authors;
		}
		public Users FindUsers(int id)
		{
			try
			{
				var users = companyCateringDbContext.Users.Where(p => p.Id == id)
					.FirstOrDefault();
				return users;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw ex;
			}
		}
		public void AddUsers(Users users)
		{
			try
			{
				companyCateringDbContext.Add(users);
				companyCateringDbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw ex;
			}
		}
		public void RemoveUsers(Users users)
		{
			try
			{
				companyCateringDbContext.Remove(users);
				companyCateringDbContext.SaveChanges();
				Console.WriteLine("Author removed");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error : " + ex.Message.ToString());
				throw ex;
			}
		}
		public void UpdateUsers(Users users)
		{
			try
			{
				var userExists = companyCateringDbContext.Users.Where(p => p.Id == users.Id)
				.FirstOrDefault();

				if (userExists == null)
				{
					throw new Exception("Record does not exists");
				}

				userExists.Name = users.Name;
				userExists.UserName = users.UserName;
				

				companyCateringDbContext.Users.Update(userExists);
				companyCateringDbContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw ex;
			}
		}
		public bool UsersExists(Users users)
		{
			return companyCateringDbContext.Users.Any(a => a.Id == users.Id);
		}
	}
}
