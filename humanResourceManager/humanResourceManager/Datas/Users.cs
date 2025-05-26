using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        // FK đến Employee
        public int? EmployeeID { get; set; } // <-- cho phép null
        public Employees? Employee { get; set; }

        [MaxLength(128)]
        public string Username { get; set; }
        [MaxLength(128)]
        public string Password { get; set; }

        public Role Role { get; set; }

		public List<int> RoleIds { get; set; } = new List<int>();
		public bool IsDeleted { get; set; } = false;

		public bool IsStatic { get; set; } = false;

		[MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }
        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
