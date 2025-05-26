using humanResourceManager.Datas;
using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.UsersModel
{
	public class UsersDto
	{
		public int Id { get; set; }

        // FK đến Employee
        public int? EmployeeID { get; set; } // <-- cho phép null
        public Employees? Employee { get; set; }

        public string? Username { get; set; }
		public string? Password { get; set; }

		public Role Role { get; set; }

		public List<int> RoleIds { get; set; } = new List<int>();
		public List<string> RoleNames { get; set; } = new List<string>();

		public List<int> PermissionIds { get; set; } = new List<int>();
		public List<string> PermissionNames { get; set; } = new List<string>();

		public bool IsDeleted { get; set; } = false;

		public bool IsStatic { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }
		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
