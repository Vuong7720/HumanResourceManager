using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.DepartmentModel;
using humanResourceManager.Models.ICurrentUser;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class DepartmentsService : IDepartmentsService
	{
		private readonly MyDbContext _dbContext;
		private readonly ICurrentUserExtended _currentUser;

		public DepartmentsService(MyDbContext dbContext, ICurrentUserExtended currentUser)
		{
			_dbContext = dbContext;
			_currentUser = currentUser;
		}

		public async Task<MessageDto> CreateAsync(CreateUpdateDepartmentsDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement_Create))
			{
				throw new Exception("Không có quyền tạo mới phòng ban");
			}

			try
			{
                Departments entity = new Departments
                {
                    DepartmentName = input.DepartmentName,
                    CreationTime = DateTime.Now
                };

                _dbContext.Departments.Add(entity);
                await _dbContext.SaveChangesAsync();
                return new MessageDto
                {
                    Status = true,
                    Message = "Thêm mới phòng ban thành công!",
                };
            }
            catch(Exception ex)
            {
                throw new BusinessException("Lỗi không xác định");
            }
		}

		public async Task DeleteAsync(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement_Delete))
			{
				throw new Exception("Không có quyền xoá phòng ban");
			}

			var entity = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Departments.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement_Delete))
			{
				throw new Exception("Không có quyền xoá phòng ban");
			}

			var entities = await _dbContext.Departments.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Departments.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<DepartmentsDto> GetById(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement))
			{
				throw new Exception("Không có quyền xem chi tiết phòng ban");
			}

			var entity = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new DepartmentsDto
			{
				Id = entity.Id,
				DepartmentName = entity.DepartmentName,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<DepartmentsDto>> GetPagingDto(PagingRequest request)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement))
			{
				throw new Exception("Không có quyền xem danh sách phòng ban");
			}

			var departments = _dbContext.Departments.AsQueryable();
			if (request.Keyword != null)
			{
				departments = departments.Where(x => x.DepartmentName.Trim().ToLower().Contains(request.Keyword.Trim().ToLower()));
			}
			var queryResult = departments
			.Select(a => new DepartmentsDto()
			{
				Id = a.Id,
				DepartmentName = a.DepartmentName,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<DepartmentsDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<DepartmentsDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		public async Task<List<SelectOptionItems>> GetselectOption()
		{
			var departments = _dbContext.Departments;

			var result = await departments
				.Select(a => new SelectOptionItems
				{
					Value = a.Id,
					Label = a.DepartmentName,
				})
				.ToListAsync();

			return result;
		}

		public async Task<DepartmentsDto> UpdateAsync(int id, CreateUpdateDepartmentsDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.DepartmentManagement_Update))
			{
				throw new Exception("Không có quyền cập nhật thông tin phòng ban");
			}

			var entity = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy phòng ban cần cập nhật");
			}

			entity.DepartmentName = input.DepartmentName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Departments.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new DepartmentsDto
			{
				Id = entity.Id,
				DepartmentName = input.DepartmentName,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}
	}
}
