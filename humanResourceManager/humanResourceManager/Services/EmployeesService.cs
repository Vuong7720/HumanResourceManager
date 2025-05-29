using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.ContractsModel;
using humanResourceManager.Models.EmployeesModel;
using humanResourceManager.Models.ICurrentUser;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class EmployeesService : IEmployeesService
	{
		private readonly MyDbContext _dbContext;
		private readonly ICurrentUserExtended _currentUser;

		public EmployeesService(MyDbContext dbContext, ICurrentUserExtended currentUser)
		{
			_dbContext = dbContext;
			_currentUser = currentUser;
		}

		public async Task<EmployeesDto> CreateAsync(CreateUpdateEmployeesDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement_Create))
			{
				throw new Exception("Không có quyền tạo mới nhân viên");
			}
			Employees entity = new Employees
			{
				FullName = input.FullName,
				BirthDay = input.BirthDay,
				Gender = input.Gender,
				PhoneNumber = input.PhoneNumber,
				Email = input.Email,
				Address = input.Address,
				PositionId = input.PositionId,
				DepartmentId = input.DepartmentId,
				Salary = input.Salary,
				HireDate = input.HireDate,
				Status = input.Status,
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			var result = _dbContext.Employees.Add(entity);
            await _dbContext.SaveChangesAsync();

            Contracts insertContract = new Contracts
			{
				EmployeeID = result.Entity.Id,
				ContractType = input.ContractType,
				StartDate = input.StartDate,
				EndDate = input.HireDate,
				Salary = input.Salary,
			};

			var resultContract = _dbContext.Contracts.Add(insertContract);


			await _dbContext.SaveChangesAsync();
			

			return new EmployeesDto
			{
				Id = entity.Id,
				FullName = entity.FullName,
				BirthDay = entity.BirthDay,
				Gender = entity.Gender,
				PhoneNumber = entity.PhoneNumber,	
				Email = entity.Email,
				Address = entity.Address,
				PositionId = entity.PositionId,
				DepartmentId = entity.DepartmentId,
				Salary = entity.Salary,
				HireDate = entity.HireDate,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement_Delete))
			{
				throw new Exception("Không có quyền xoá nhân viên");
			}

			var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Employees.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement_Delete))
			{
				throw new Exception("Không có quyền xoá nhân viên");
			}

			var entities = await _dbContext.Employees.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Employees.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<EmployeesDto> GetById(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement))
			{
				throw new Exception("Không có quyền xem thông tin chi tiết nhân viên");
			}

			var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new EmployeesDto
			{
				Id = entity.Id,
				FullName = entity.FullName,
				BirthDay = entity.BirthDay,
				Gender = entity.Gender,
				PhoneNumber = entity.PhoneNumber,
				Email = entity.Email,
				Address = entity.Address,
				PositionId = entity.PositionId,
				DepartmentId = entity.DepartmentId,
				Salary = entity.Salary,
				HireDate = entity.HireDate,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<EmployeesDto>> GetPagingDto(PagingRequest request)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement))
			{
				throw new Exception("Không có quyền xem danh sách nhân viên");
			}

			var employees = _dbContext.Employees.AsQueryable();
			if (!string.IsNullOrWhiteSpace(request.Keyword))
			{
				employees = employees.Where(x => x.FullName.Trim().ToLower().Contains(request.Keyword.Trim().ToLower()));
			}
			var queryResult = employees
			.Select(entity => new EmployeesDto()
			{
				Id = entity.Id,
				FullName = entity.FullName,
				BirthDay = entity.BirthDay,
				Gender = entity.Gender,
				PhoneNumber = entity.PhoneNumber,
				Email = entity.Email,
				Address = entity.Address,
				PositionId = entity.PositionId,
				DepartmentId = entity.DepartmentId,
				Salary = entity.Salary,
				HireDate = entity.HireDate,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			});

			var pagingData = await PageResult<EmployeesDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<EmployeesDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		public async Task<List<SelectOptionItems>> GetselectOption()
		{
			var employees = _dbContext.Employees;

			var result = await employees
				.Select(a => new SelectOptionItems
				{
					Value = a.Id,
					Label = a.FullName,
				})
				.ToListAsync();

			return result;
		}

		public async Task<EmployeesDto> UpdateAsync(int id, CreateUpdateEmployeesDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.EmployeeManagement_Update))
			{
				throw new Exception("Không có quyền cập nhật thông tin nhân viên");
			}

			var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi cần cập nhật");
			}

			entity.FullName = input.FullName;
			entity.BirthDay = input.BirthDay;
			entity.Gender = input.Gender;
			entity.PhoneNumber = input.PhoneNumber;
			entity.Email = input.Email;
			entity.Address = input.Address;
			entity.PositionId = input.PositionId;
			entity.DepartmentId = input.DepartmentId;
			entity.Salary = input.Salary;
			entity.HireDate = input.HireDate;
			entity.Status = input.Status;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Employees.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new EmployeesDto
			{
				Id = entity.Id,
				FullName = entity.FullName,
				BirthDay = entity.BirthDay,
				Gender = entity.Gender,
				PhoneNumber = entity.PhoneNumber,
				Email = entity.Email,
				Address = entity.Address,
				PositionId = entity.PositionId,
				DepartmentId = entity.DepartmentId,
				Salary = entity.Salary,
				HireDate = entity.HireDate,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}
	}
}
