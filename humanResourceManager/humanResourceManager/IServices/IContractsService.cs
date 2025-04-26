using humanResourceManager.Models;
using humanResourceManager.Models.ContractsModel;

namespace humanResourceManager.IServices
{
	public interface IContractsService
	{
		Task<ContractsDto> CreateAsync(CreateUpdateContractsDto input);

		Task<ContractsDto> UpdateAsync(int id, CreateUpdateContractsDto input);

		Task<PagedResultDto<ContractsDto>> GetPagingDto(PagingRequest request);
		Task<ContractsDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);
	}
}
