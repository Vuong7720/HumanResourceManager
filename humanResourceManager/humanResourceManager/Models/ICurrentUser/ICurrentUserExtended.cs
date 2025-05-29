namespace humanResourceManager.Models.ICurrentUser
{
	public interface ICurrentUserExtended
	{
		Guid? UserId { get; }
		string? UserName { get; }
		List<string> PermissionNames { get; }
	}
}
