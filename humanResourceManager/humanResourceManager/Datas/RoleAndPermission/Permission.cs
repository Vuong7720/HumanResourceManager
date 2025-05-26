using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas.RoleAndPermission
{
	public class Permission
	{
		[Key]
		public int Id { get; set; }
		public string? PermissionName { get; set; }

		public DateTime? CreationTime { get; set; }
	}
}
