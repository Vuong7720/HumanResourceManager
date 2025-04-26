using humanResourceManager.Datas;
using humanResourceManager.Enums;

namespace humanResourceManager.Models.UsersModel
{
	public class CreateUpdateUsersDto
	{
		public int EmployeeID { get; set; }
		//public Employees Employee { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }

		public Role Role { get; set; }

		public string? UserName { get; set; }
		public int UserId { get; set; }
	}
}
