using humanResourceManager.IServices;
using humanResourceManager.Models.PositionsModel;
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
				return Ok(ApiResult.Success(await _attendanceService.CreateAsync(input), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpDelete("delete")]
		public async Task Delete(int id)
		{
			await _attendanceService.DeleteAsync(id);
		}

		[HttpDelete("delete-by-ids")]
		public async Task DeleteByIds(List<int> ids)
		{
			await _attendanceService.DeleteMultipleAsync(ids);
		}

	}
}
