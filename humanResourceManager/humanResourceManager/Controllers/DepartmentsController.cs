using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.DepartmentModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/department")]
	public class DepartmentsController : ControllerBase
	{
		private readonly IDepartmentsService _service;

		public DepartmentsController(IDepartmentsService service)
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
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdateDepartmentsDto model)
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
		public async Task<IActionResult> CreateAsync(CreateUpdateDepartmentsDto input)
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
		public async Task Delete(int id)
		{
			await _service.DeleteAsync(id);
		}

		[HttpDelete("delete-by-ids")]
		public async Task DeleteByIds(List<int> ids)
		{
			await _service.DeleteMultipleAsync(ids);
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
	}
}
