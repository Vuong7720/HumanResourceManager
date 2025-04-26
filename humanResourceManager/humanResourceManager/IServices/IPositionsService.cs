using humanResourceManager.Models;
using humanResourceManager.Models.PositionsModel;

namespace humanResourceManager.IServices
{
	public interface IPositionsService
	{
		Task<PositionsDto> CreateAsync(CreateUpdatePositionsDto input);

		Task<PositionsDto> UpdateAsync(int id, CreateUpdatePositionsDto input);

		Task<PagedResultDto<PositionsDto>> GetPagingDto(PagingRequest request);
		Task<PositionsDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);

		Task<List<SelectOptionItems>> GetselectOption();
	}
}
