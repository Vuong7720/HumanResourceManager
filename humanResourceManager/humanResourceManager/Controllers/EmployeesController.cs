using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.EmployeesModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/employee")]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeesService _service;

		public EmployeesController(IEmployeesService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("get-list-dto")]
		public async Task<IActionResult> GetListAsync(PagingRequest input)
		{
			try
			{
				return Ok(ApiResult.Success(await _service.GetPagingDto(input), "Success!"));
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
				return Ok(ApiResult.Success(await _service.GetById(id), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdateEmployeesDto model)
		{
			try
			{
				return Ok(ApiResult.Success(await _service.UpdateAsync(id, model), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAsync(CreateUpdateEmployeesDto input)
		{
			try
			{
				return Ok(ApiResult.Success(await _service.CreateAsync(input), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.DeleteAsync(id);
			return Ok(ApiResult.Success(null, "Xóa thành công"));
		}

		[HttpDelete("delete-by-ids")]
		public async Task<IActionResult> DeleteByIds(List<int> ids)
		{
			await _service.DeleteMultipleAsync(ids);
			return Ok(ApiResult.Success(null, "Xóa thành công"));		
		}

		[HttpGet("get-list-select")]
		public async Task<IActionResult> GetListSelect()
		{
			try
			{
				return Ok(ApiResult.Success(await _service.GetselectOption(), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("export-excel")]
		public async Task<IActionResult> ExportExcel(PagingRequest request)
		{
			var execlBytes = await _service.ExportDataReport(request);
			var file = File(execlBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			return file;
		}
	}
}
