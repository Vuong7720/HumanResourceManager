using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.ICurrentUser;
using humanResourceManager.Models.PositionsModel;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace humanResourceManager.Services
{
	public class PositionsService : IPositionsService
	{
		private readonly MyDbContext _dbContext;
		private readonly ICurrentUserExtended _currentUser;

		public PositionsService(MyDbContext dbContext, ICurrentUserExtended currentUser)
		{
			_dbContext = dbContext;
			_currentUser = currentUser;
		}

		public async Task<PositionsDto> CreateAsync(CreateUpdatePositionsDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement_Create))
			{
				throw new Exception("Không có quyền tạo mới chức vụ");
			}

			Positions entity = new Positions
			{
				PositionName = input.PositionName,
				CreationTime = DateTime.Now
			};

			_dbContext.Positions.Add(entity);
			await _dbContext.SaveChangesAsync();
			return new PositionsDto
			{
				Id = entity.Id,
				PositionName = entity.PositionName,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement_Delete))
			{
				throw new Exception("Không có quyền xoá mới chức vụ");
			}

			var entity = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Positions.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement_Delete))
			{
				throw new Exception("Không có quyền xoá mới chức vụ");
			}

			var entities = await _dbContext.Positions.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Positions.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<PositionsDto> GetById(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement))
			{
				throw new Exception("Không có quyền xem thông tin chi tiết chức vụ");
			}

			var entity = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new PositionsDto
			{
				Id = entity.Id,
				PositionName = entity.PositionName,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<PositionsDto>> GetPagingDto(PagingRequest request)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement))
			{
				throw new Exception("Không có quyền xem danh sách chức vụ");
			}

			var positions = _dbContext.Positions.AsQueryable();
			if(request.Keyword != null)
			{
				positions = positions.Where(x => x.PositionName.Trim().ToLower().Contains(request.Keyword.Trim().ToLower()));
			}
			var queryResult = positions
			.Select(a => new PositionsDto()
			{
				Id = a.Id,
				PositionName = a.PositionName,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<PositionsDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<PositionsDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		public async Task<List<SelectOptionItems>> GetselectOption()
		{
			var positions = _dbContext.Positions;

			var result = await positions
				.Select(a => new SelectOptionItems
				{
					Value = a.Id,
					Label = a.PositionName,
				})
				.ToListAsync();

			return result;
		}

		public async Task<PositionsDto> UpdateAsync(int id, CreateUpdatePositionsDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.PositionManagement_Update))
			{
				throw new Exception("Không có quyền cập nhật thông tin chức vụ");
			}

			var entity = await _dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi cần cập nhật");
			}

			entity.PositionName = input.PositionName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Positions.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new PositionsDto
			{
				Id = entity.Id,
				PositionName = entity.PositionName,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}
	}
}
