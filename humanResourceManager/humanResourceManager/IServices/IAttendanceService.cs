using humanResourceManager.Models;
using humanResourceManager.Models.AttendanceModel;

namespace humanResourceManager.IServices
{
	public interface IAttendanceService
	{
		Task<AttendanceDto> CreateAsync(CreateUpdateAttendanceDto input);

		Task<AttendanceDto> UpdateAsync(int id, CreateUpdateAttendanceDto input);

		Task<PagedResultDto<AttendanceDto>> GetPagingDto(PagingRequest request);
		Task<AttendanceDto> GetById(int id);

		Task DeleteAsync(int id);

		Task DeleteMultipleAsync(IEnumerable<int> ids);
	}
}
