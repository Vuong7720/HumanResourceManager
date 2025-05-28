using humanResourceManager.Datas;
using humanResourceManager.Enums;

namespace humanResourceManager.Models.UsersModel
{
	public class CreateUpdateUsersDto
	{
		public int? EmployeeID { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }

		public Role? Role { get; set; }
		public List<int>? RoleIds { get; set; } = new List<int>();

	}
}
