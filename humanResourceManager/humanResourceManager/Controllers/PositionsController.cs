using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.PositionsModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/position")]
	public class PositionsController : ControllerBase
	{
		private readonly IPositionsService _positionsService;

		public PositionsController(IPositionsService positionsService)
		{
			_positionsService = positionsService;
		}

		[HttpPost]
		[Route("get-list-dto")]
		public async Task<IActionResult> GetListAsync(PagingRequest input)
		{
			try
			{
				return Ok(ApiResult.Success(await _positionsService.GetPagingDto(input), "Success!"));
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
				return Ok(ApiResult.Success(await _positionsService.GetById(id), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdatePositionsDto model)
		{
			try
			{
				return Ok(ApiResult.Success(await _positionsService.UpdateAsync(id, model), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAsync(CreateUpdatePositionsDto input)
		{
			try
			{
				return Ok(ApiResult.Success(await _positionsService.CreateAsync(input), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete(int id)
		{
			await _positionsService.DeleteAsync(id);
			return Ok(ApiResult.Success(null, "Xóa thành công"));
		}

		[HttpDelete("delete-by-ids")]
		public async Task<IActionResult> DeleteByIds(List<int> ids)
		{
			await _positionsService.DeleteMultipleAsync(ids);
			return Ok(ApiResult.Success(null, "Xóa thành công"));
		}

		[HttpGet("get-list-select")]
		public async Task<IActionResult> GetListSelect()
		{
			try
			{
				return Ok(ApiResult.Success(await _positionsService.GetselectOption(), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}
	}
}
