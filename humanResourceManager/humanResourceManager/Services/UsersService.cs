using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.UsersModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class UsersService : IUsersService
	{
		private readonly MyDbContext _dbContext;
        private readonly JwtService _jwtService;

        public UsersService(MyDbContext dbContext, JwtService jwtService)
		{
			_dbContext = dbContext;
            _jwtService = jwtService;
        }

		public async Task<UsersDto> CreateAsync(CreateUpdateUsersDto input)
		{
			Users entity = new Users
			{
				EmployeeID = input.EmployeeID,
				// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
				Username = input.Username,
				Password = input.Password,
				Role = input.Role,
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			_dbContext.Users.Add(entity);
			await _dbContext.SaveChangesAsync();
			return new UsersDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Username = entity.Username,
				Password = entity.Password,
				Role = entity.Role,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			_dbContext.Users.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			var entities = await _dbContext.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			_dbContext.Users.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<UsersDto> GetById(int id)
		{
			var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu chấm công");
			}

			return new UsersDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Username = entity.Username,
				Password = entity.Password,
				Role = entity.Role,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<UsersDto>> GetPagingDto(PagingRequest request)
		{
			var users = _dbContext.Users.AsQueryable();

			var queryResult = users
			.Select(a => new UsersDto()
			{
				Id = a.Id,
				EmployeeID = a.EmployeeID,
				Employee = a.Employee,
				Username = a.Username,
				Password = a.Password,
				Role = a.Role,
				IsDeleted = a.IsDeleted,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<UsersDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			return new PagedResultDto<UsersDto>(pagingData.TotalCount, pagingData.Items.ToList());
		}

		public async Task<UsersDto> SignIn(SignInInfo input)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Username == input.Username && a.Password == input.Password);

			if (user != null)
			{
				return new UsersDto()
				{
					Id = user.Id,
					EmployeeID = user.EmployeeID,
					Employee = user.Employee,
					Username = user.Username,
					Password = user.Password,
					Role = user.Role,
					IsDeleted = user.IsDeleted,
					CreationName = user.CreationName,
					CreationTime = user.CreationTime,
					UpdatedBy = user.UpdatedBy,
					UpdatedAt = user.UpdatedAt,
				};
			}
			else
			{
				return null;
			}
		}

		public async Task<UsersDto> UpdateAsync(int id, CreateUpdateUsersDto input)
		{
			var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy bản ghi Attendance cần cập nhật");
			}

			entity.EmployeeID = input.EmployeeID;
			// entity.Employee = input.Employee; // Không set lại Employee object ở Update, chỉ giữ EmployeeID
			entity.Username = input.Username;
			entity.Password = input.Password;
			entity.Role = input.Role;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Users.Update(entity);
			await _dbContext.SaveChangesAsync();

			return new UsersDto
			{
				Id = entity.Id,
				EmployeeID = entity.EmployeeID,
				Employee = entity.Employee,
				Username = entity.Username,
				Password = entity.Password,
				Role = entity.Role,
				IsDeleted = entity.IsDeleted,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return _jwtService.GenerateToken(user);
        }

        public async Task<MessageDto> RegisterAsync(RegisterDto dto)
        {
			try
			{
                if (await _dbContext.Users.AnyAsync(u => u.Username == dto.Username))
                    throw new Exception("Username already exists");

                var user = new Users
                {
                    Username = dto.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    EmployeeID = dto.EmployeeID,
                    Role = Role.Employee,
                    CreationTime = DateTime.Now
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return new MessageDto
                {
                    Status = true,
                    Message = "Đăng ký thành công !"
                };
            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
            
        }


    }
}
