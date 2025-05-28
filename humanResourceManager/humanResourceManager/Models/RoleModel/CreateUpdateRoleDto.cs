
namespace humanResourceManager.Models.RoleModel
{
	public class CreateUpdateRoleDto
	{
		public string? RoleName { get; set; }

		public List<int>? PermissionIds { get; set; } = new List<int>();

		public string? UserName { get; set; }
	}
}
