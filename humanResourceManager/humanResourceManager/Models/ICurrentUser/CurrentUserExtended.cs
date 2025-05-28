using System.Security.Claims;

namespace humanResourceManager.Models.ICurrentUser
{
	public class CurrentUserExtended : ICurrentUserExtended
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserExtended(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public Guid? UserId =>
			Guid.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

		public string? UserName =>
			_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;

		public List<string> PermissionNames
		{
			get
			{
				var x = _httpContextAccessor.HttpContext;
				var y = _httpContextAccessor.HttpContext?.User;
				var raw = _httpContextAccessor.HttpContext?.User.FindFirst("permission_names")?.Value;
				return raw?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
			}
		}
	}
}
