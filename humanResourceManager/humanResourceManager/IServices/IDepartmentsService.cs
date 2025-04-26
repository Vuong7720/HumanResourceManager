using humanResourceManager.Models;
using humanResourceManager.Models.DepartmentModel;

namespace humanResourceManager.IServices
{
	public interface IDepartmentsService
	{
		Task<DepartmentsDto> CreateAsync(CreateUpdateDepartmentsDto input);

		Task<DepartmentsDto> UpdateAsync(int id, CreateUpdateDepartmentsDto input);

		Task<PagedResultDto<DepartmentsDto>> GetPagingDto(PagingRequest request);
		Task<DepartmentsDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);

		Task<List<SelectOptionItems>> GetselectOption();
	}
}
