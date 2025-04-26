using humanResourceManager.Models;
using humanResourceManager.Models.EmployeesModel;

namespace humanResourceManager.IServices
{
	public interface IEmployeesService
	{
		Task<EmployeesDto> CreateAsync(CreateUpdateEmployeesDto input);

		Task<EmployeesDto> UpdateAsync(int id, CreateUpdateEmployeesDto input);

		Task<PagedResultDto<EmployeesDto>> GetPagingDto(PagingRequest request);
		Task<EmployeesDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);

		Task<List<SelectOptionItems>> GetselectOption();
	}
}
