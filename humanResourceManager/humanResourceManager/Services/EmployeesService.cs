using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.ContractsModel;
using humanResourceManager.Models.EmployeesModel;
using humanResourceManager.Models.ICurrentUser;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace humanResourceManager.Services
{
	public class EmployeesService : IEmployeesService
	{
		private readonly MyDbContext _dbContext;
		private readonly ICurrentUserExtended _currentUser;
		private readonly IHostingEnvironment _hostingEnvironment;

		public EmployeesService(MyDbContext dbContext, ICurrentUserExtended currentUser, IHostingEnvironment hostingEnvironment)
		{
			_dbContext = dbContext;
			_currentUser = currentUser;
			_hostingEnvironment = hostingEnvironment;
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

		#region export data
		public async Task<byte[]> ExportDataReport(PagingRequest request)
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

			return await WriteDataToExcel(queryResult.ToList(), request);
		}

		private async Task<byte[]> WriteDataToExcel(List<EmployeesDto> data, PagingRequest request)
		{
			try
			{
				var file = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath + "/ExcelTemplates/ListEmployee.xlsx"));

				if (!file.Exists)
				{
					throw new FileNotFoundException("Không tìm thấy file template.");
				}

				using var xlPackage = new ExcelPackage(file);
				var ngayXuat = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
				var tieuDe = "DANH SÁCH NHÂN VIÊN: ";
				if (!string.IsNullOrEmpty(request.Keyword))
				{
					tieuDe = tieuDe + "Lọc theo từ khóa " + request.Keyword;
				}

				foreach (var index in Enumerable.Range(0, 1))
				{
					ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
					var workbook = xlPackage.Workbook;
					var worksheet = workbook.Worksheets["Tổng hợp"];
					worksheet.Cells[1, 1].Value = tieuDe;
					worksheet.Cells[2, 1].Value = ngayXuat;
					var cellTable = worksheet?.Cells.FirstOrDefault();

					var rowStart = 5;

					if (cellTable != null && index == 0 && data.Count > 0)
					{
						var stt = 1;
						foreach (var item in data)
						{
							for (var i = 1; i <= 12; i++)
							{
								worksheet.Cells[rowStart, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
							}
							worksheet.Cells[rowStart, 1].Value = stt;
							worksheet.Cells[rowStart, 2].Value = item.FullName;
							worksheet.Cells[rowStart, 3].Value = item.BirthDay;
							worksheet.Cells[rowStart, 4].Value = item.Gender == 1 ? "Nam" : "Nữ";
							worksheet.Cells[rowStart, 5].Value = item.PhoneNumber;
							worksheet.Cells[rowStart, 6].Value = item.Email;
							worksheet.Cells[rowStart, 7].Value = item.Address;
							worksheet.Cells[rowStart, 8].Value = item.PositionName;
							worksheet.Cells[rowStart, 9].Value = item.DepartmentName;
							worksheet.Cells[rowStart, 10].Value = item.Salary;
							worksheet.Cells[rowStart, 11].Value = item.HireDate;
							worksheet.Cells[rowStart, 12].Value = item.Status == EmployeeStatus.DangLamViec ? "Đang làm việc" : (item.Status == EmployeeStatus.TamNghi ? "Tạm nghỉ" : "Thôi việc");
							stt++;
							rowStart++;
						}
					}
				}
				await Task.Yield();
				var dataFile = xlPackage.GetAsByteArray();
				return dataFile;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion
	}
}
