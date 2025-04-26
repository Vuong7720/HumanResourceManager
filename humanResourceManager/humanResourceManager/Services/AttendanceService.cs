using humanResourceManager.Datas;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.AttendanceModel;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class AttendanceService : IAttendanceService
	{
		private readonly MyDbContext _dbContext;

		public AttendanceService(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<AttendanceDto> CreateAsync(CreateUpdateAttendanceDto input)
		{
			Attendance entity = new Attendance
			{
				EmployeeID = input.EmployeeID,
				// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
				Date = input.Date,
				CheckIn = input.CheckIn,
				CheckOut = input.CheckOut,
				Status = input.Status,
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			_dbContext.Attendance.Add(entity);
			await _dbContext.SaveChangesAsync();
			return new AttendanceDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Date = entity.Date,
				CheckIn = entity.CheckIn,
				CheckOut = entity.CheckOut,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<AttendanceDto> UpdateAsync(int id, CreateUpdateAttendanceDto input)
		{
			var entity = await _dbContext.Attendance.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi Attendance cần cập nhật");
			}

			entity.EmployeeID = input.EmployeeID;
			// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
			entity.Date = input.Date;
			entity.CheckIn = input.CheckIn;
			entity.CheckOut = input.CheckOut;
			entity.Status = input.Status;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Attendance.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new AttendanceDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Date = entity.Date,
				CheckIn = entity.CheckIn,
				CheckOut = entity.CheckOut,
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
			var entity = await _dbContext.Attendance.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Attendance.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			var entities = await _dbContext.Attendance.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Attendance.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<AttendanceDto> GetById(int id)
		{
			var entity = await _dbContext.Attendance.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new AttendanceDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Date = entity.Date,
				CheckIn = entity.CheckIn,
				CheckOut = entity.CheckOut,
				Status = entity.Status,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<AttendanceDto>> GetPagingDto(PagingRequest request)
		{
			var attendances = _dbContext.Attendance.AsQueryable();

			var queryResult = attendances
			.Select(a => new AttendanceDto()
			{
				Id = a.Id,
				EmployeeID = a.EmployeeID,
				Employee = a.Employee,
				Date = a.Date,
				CheckIn = a.CheckIn,
				CheckOut = a.CheckOut,
				Status = a.Status,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<AttendanceDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<AttendanceDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}


	}
}
