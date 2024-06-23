using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.DataLayer.Repositories
{
	public interface IUserRepository
	{
		public List<Users> GetAllUsers();
		public Users FindUsers(int id);
		public void AddUsers(Users users);
		public void RemoveUsers(Users users);
		public void UpdateUsers(Users users);
		public bool UsersExists(Users users);
	}
}

