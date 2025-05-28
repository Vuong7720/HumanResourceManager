using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;
using humanResourceManager.Models.RoleModel;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/role")]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _service;

		public RoleController(IRoleService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("get-list-dto")]
		public async Task<IActionResult> GetListAsync([FromBody] PagingRequest input)
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
		public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateUpdateRoleDto model)
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
		public async Task<IActionResult> CreateAsync([FromBody] CreateUpdateRoleDto input)
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

		[HttpGet("get-list-select-role")]
		public async Task<IActionResult> GetListSelectRole()
		{
			try
			{
				return Ok(ApiResult.Success(await _service.GetselectOptionRole(), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpGet("get-list-select-permission")]
		public async Task<IActionResult> GetListSelectPermission()
		{
			try
			{
				return Ok(ApiResult.Success(await _service.GetselectOptionPermission(), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}
	}
}
