using humanResourceManager.Datas;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.ContractsModel;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class ContractsService : IContractsService
	{
		private readonly MyDbContext _dbContext;

		public ContractsService(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ContractsDto> CreateAsync(CreateUpdateContractsDto input)
		{
			Contracts entity = new Contracts
			{
				EmployeeID = input.EmployeeID,
				// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
				ContractType = input.ContractType,
				StartDate = input.StartDate,
				EndDate = input.EndDate,
				Salary = input.Salary,
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			_dbContext.Contracts.Add(entity);
			await _dbContext.SaveChangesAsync();
			return new ContractsDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				ContractType = entity.ContractType,
				StartDate = entity.StartDate,
				EndDate = entity.EndDate,
				Salary = entity.Salary,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<ContractsDto> UpdateAsync(int id, CreateUpdateContractsDto input)
		{
			var entity = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi Attendance cần cập nhật");
			}

			entity.EmployeeID = input.EmployeeID;
			// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
			entity.ContractType = input.ContractType;
			entity.StartDate = input.StartDate;
			entity.EndDate = input.EndDate;
			entity.Salary = input.Salary;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Contracts.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new ContractsDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				ContractType = entity.ContractType,
				StartDate = entity.StartDate,
				EndDate = entity.EndDate,
				Salary = entity.Salary,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Contracts.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			var entities = await _dbContext.Contracts.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Contracts.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<ContractsDto> GetById(int id)
		{
			var entity = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new ContractsDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				ContractType = entity.ContractType,
				StartDate = entity.StartDate,
				EndDate = entity.EndDate,
				Salary = entity.Salary,
				IsDeleted = entity.IsDeleted
			};
		}

		public async Task<PagedResultDto<ContractsDto>> GetPagingDto(PagingRequest request)
		{
			var contracts = _dbContext.Contracts.AsQueryable();

			var queryResult = contracts
			.Select(a => new ContractsDto()
			{
				Id = a.Id,
				EmployeeID = a.EmployeeID,
				Employee = a.Employee,
				ContractType = a.ContractType,
				StartDate = a.StartDate,
				EndDate = a.EndDate,
				Salary = a.Salary,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<ContractsDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<ContractsDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		
	}
}
