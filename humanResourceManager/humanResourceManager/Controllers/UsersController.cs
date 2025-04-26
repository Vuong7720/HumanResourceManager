using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.UsersModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;

namespace humanResourceManager.Controllers
{
	[ApiController]
	[Route("api/user")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		[HttpPost]
		[Route("get-list-dto")]
		public async Task<IActionResult> GetListAsync(PagingRequest input)
		{
			try
			{
				return Ok(ApiResult.Success(await _usersService.GetPagingDto(input), "Success!"));
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
				return Ok(ApiResult.Success(await _usersService.GetById(id), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPut]
		[Route("update")]
		public async Task<IActionResult> UpdateAsync(int id, CreateUpdateUsersDto model)
		{
			try
			{
				return Ok(ApiResult.Success(await _usersService.UpdateAsync(id, model), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateAsync(CreateUpdateUsersDto input)
		{
			try
			{
				return Ok(ApiResult.Success(await _usersService.CreateAsync(input), "Success!"));
			}
			catch (BusinessException ex)
			{
				return Ok(ApiResult.Error(ex.Message ?? "Lỗi không xác định"));
			}
		}

		[HttpDelete("delete")]
		public async Task Delete(int id)
		{
			await _usersService.DeleteAsync(id);
		}

		[HttpDelete("delete-by-ids")]
		public async Task DeleteByIds(List<int> ids)
		{
			await _usersService.DeleteMultipleAsync(ids);
		}
	}
}
