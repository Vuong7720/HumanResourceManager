using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas.RoleAndPermission
{
	public class Rolee
	{
		[Key]
		public int Id { get; set; }
		public string? RoleName { get; set; }

		public List<int> PermissionIds { get; set; } = new List<int>();

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
