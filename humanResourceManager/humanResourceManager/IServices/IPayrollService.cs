using humanResourceManager.Models;
using humanResourceManager.Models.PayrollModel;

namespace humanResourceManager.IServices
{
	public interface IPayrollService
	{
		Task<PayrollDto> CreateAsync(CreateUpdatePayrollDto input);

		Task<PayrollDto> UpdateAsync(int id, CreateUpdatePayrollDto input);

		Task<PagedResultDto<PayrollDto>> GetPagingDto(PagingRequest request);
		Task<PayrollDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);
	}
}
