using humanResourceManager.Datas;
using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.UsersModel
{
	public class UsersDto
	{
		public int Id { get; set; }

		// FK đến Employee
		public int EmployeeID { get; set; }
		public Employees? Employee { get; set; }

		public string? Username { get; set; }
		public string? Password { get; set; }

		public Role Role { get; set; }
		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }
		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
