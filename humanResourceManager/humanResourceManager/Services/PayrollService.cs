using humanResourceManager.Datas;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.PayrollModel;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class PayrollService : IPayrollService
	{
		private readonly MyDbContext _dbContext;

		public PayrollService(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<PayrollDto> CreateAsync(CreateUpdatePayrollDto input)
		{
			Payroll entity = new Payroll
			{
				EmployeeID = input.EmployeeID,
				// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
				Month = input.Month,
				Year = input.Year,
				BaseSalary = input.BaseSalary,
				Bonus = input.Bonus,
				Deductions = input.Deductions,
				NetSalary = input.NetSalary,
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			_dbContext.Payroll.Add(entity);
			await _dbContext.SaveChangesAsync();
			return new PayrollDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Month = entity.Month,
				Year = entity.Year,
				BaseSalary = entity.BaseSalary,
				Bonus = entity.Bonus,
				Deductions = entity.Deductions,
				NetSalary = entity.NetSalary,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbContext.Payroll.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Payroll.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			var entities = await _dbContext.Payroll.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Payroll.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<PayrollDto> GetById(int id)
		{
			var entity = await _dbContext.Payroll.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new PayrollDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Month = entity.Month,
				Year = entity.Year,
				BaseSalary = entity.BaseSalary,
				Bonus = entity.Bonus,
				Deductions = entity.Deductions,
				NetSalary = entity.NetSalary,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<PayrollDto>> GetPagingDto(PagingRequest request)
		{
			var payrolls = _dbContext.Payroll.AsQueryable();

			var queryResult = payrolls
			.Select(a => new PayrollDto()
			{
				Id = a.Id,
				EmployeeID = a.EmployeeID,
				Employee = a.Employee,
				Month = a.Month,
				Year = a.Year,
				BaseSalary = a.BaseSalary,
				Bonus = a.Bonus,
				Deductions = a.Deductions,
				NetSalary = a.NetSalary,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<PayrollDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<PayrollDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		public async Task<PayrollDto> UpdateAsync(int id, CreateUpdatePayrollDto input)
		{
			var entity = await _dbContext.Payroll.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi Attendance cần cập nhật");
			}

			entity.EmployeeID = input.EmployeeID;
			// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
			entity.Month = input.Month;
			entity.Year = input.Year;
			entity.BaseSalary = input.BaseSalary;
			entity.Bonus = input.Bonus;
			entity.Deductions = input.Deductions;
			entity.NetSalary = input.NetSalary;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Payroll.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new PayrollDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Month = entity.Month,
				Year = entity.Year,
				BaseSalary = entity.BaseSalary,
				Bonus = entity.Bonus,
				Deductions = entity.Deductions,
				NetSalary = entity.NetSalary,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}
	}
}
