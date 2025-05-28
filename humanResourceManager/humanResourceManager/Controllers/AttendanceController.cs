using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;
using humanResourceManager.Models.AttendanceModel;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/attendance")]
	public class AttendanceController : ControllerBase
	{
		private readonly IAttendanceService _attendanceService;
		
		public AttendanceController(IAttendanceService attendanceService)
		{
			_attendanceService = attendanceService;
		}

		[HttpPost]
		[Route("get-list-dto")]
		public async Task<IActionResult> GetListAsync(PagingRequest input)
		{
			try
			{
				return Ok(ApiResult.Success(await _attendanceService.GetPagingDto(input), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpGet]
		[Route("get-by-id")]
		public async Task<IActionResult> GetAsync(int id)
		{
			try
			{
				return Ok(ApiResult.Success(await _attendanceService.GetById(id), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdateAttendanceDto model)
		{
			try
			{
				return Ok(ApiResult.Success(await _attendanceService.UpdateAsync(id, model), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAsync(CreateUpdateAttendanceDto input)
		{
			try
			{
				return Ok(ApiResult.Success(await _attendanceService.CreateAsync(input), "Điểm danh thành công!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("check-out")]
		public async Task<IActionResult> CheckOutAsync(int employeeId)
		{
			try
			{
				return Ok(ApiResult.Success(await _attendanceService.CheckOutAsync(employeeId), "Điểm danh ra về thành công!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete(int id)
		{
			await _attendanceService.DeleteAsync(id);
			return Ok(ApiResult.Success(null, "Xóa thành công"));
		}

		[HttpDelete("delete-by-ids")]
		public async Task<IActionResult> DeleteByIds(List<int> ids)
		{
			await _attendanceService.DeleteMultipleAsync(ids);
			return Ok(ApiResult.Success(null, "Xóa thành công"));
		}

	}
}
