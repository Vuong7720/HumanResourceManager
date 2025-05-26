using humanResourceManager.Models;
using humanResourceManager.Models.RoleModel;

namespace humanResourceManager.IServices
{
	public interface IRoleService
	{
		Task<RoleDto> CreateAsync(CreateUpdateRoleDto input);

		Task<RoleDto> UpdateAsync(int id, CreateUpdateRoleDto input);

		Task<PagedResultDto<RoleDto>> GetPagingDto(PagingRequest request);
		Task<RoleDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);

		Task<List<SelectOptionItems>> GetselectOptionRole();

		// permission
		Task<List<SelectOptionItems>> GetselectOptionPermission();

		// seed data first

		Task SeedDataOriginal();
	}
}
