using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.PayrollModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/payroll")]
	public class PayrollController : ControllerBase
	{
		private readonly IPayrollService _service;

		public PayrollController(IPayrollService service)
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
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdatePayrollDto model)
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
		public async Task<IActionResult> CreateAsync(CreateUpdatePayrollDto input)
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
	}
}
