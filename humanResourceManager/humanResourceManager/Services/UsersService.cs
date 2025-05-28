using Azure.Core;
using humanResourceManager.Datas;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.UsersModel;
using humanResourceManager.Ulity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
				Role = input.Role??Role.Admin,
				RoleIds = input.RoleIds ?? new List<int>(),
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
				RoleIds = entity.RoleIds,
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

			if (entity.IsStatic == true)
			{
				throw new Exception("Không thể xoá người dùng mặc định");
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

			foreach (var entity in entities)
			{
				if (entity.IsStatic == true)
				{
					throw new BusinessException($"Không thể xoá người dùng mặc định: {entity.Username}");
				}
			}

			_dbContext.Users.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<UsersDto> GetById(int id)
		{
			var entity = await _dbContext.Users
		.Include(u => u.Employee)  // load employee nếu có
		.FirstOrDefaultAsync(x => x.Id == id);

			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu người dùng");
			}

			// Lấy role info dựa trên RoleIds của user
			var roles = await _dbContext.Roles
				.Where(r => entity.RoleIds.Contains(r.Id))
				.ToListAsync();

			// Lấy permission Ids từ các role của user
			var permissionIds = roles.SelectMany(r => r.PermissionIds).Distinct().ToList();

			// Lấy permission names từ permission Ids
			var permissions = await _dbContext.Permissions
				.Where(p => permissionIds.Contains(p.Id))
				.ToListAsync();

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

				RoleIds = roles.Select(r => r.Id).ToList(),
				RoleNames = roles.Select(r => r.RoleName ?? "").ToList(),

				PermissionIds = permissionIds,
				PermissionNames = permissions.Select(p => p.PermissionName ?? "").ToList()
			};
		}

		public async Task<PagedResultDto<UsersDto>> GetPagingDto(PagingRequest request)
		{
			var usersQuery = _dbContext.Users.AsQueryable();

			if (!string.IsNullOrWhiteSpace(request.Keyword))
			{
				var keyword = request.Keyword.Trim().ToLower();
				usersQuery = usersQuery.Where(x => x.Username.ToLower().Contains(keyword));
			}

			// Lấy dữ liệu paging users
			var pagingData = await PageResult<Users>.PageAsync(usersQuery, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);

			var users = pagingData.Items;

			// Lấy tất cả RoleId trong tập users đã lấy
			var allRoleIds = users.SelectMany(u => u.RoleIds).Distinct().ToList();

			// Lấy các role tương ứng
			var roles = await _dbContext.Roles.Where(r => allRoleIds.Contains(r.Id)).ToListAsync();

			// Lấy tất cả permissionId của các role
			var allPermissionIds = roles.SelectMany(r => r.PermissionIds).Distinct().ToList();

			// Lấy permission
			var permissions = await _dbContext.Permissions.Where(p => allPermissionIds.Contains(p.Id)).ToListAsync();

			// Map từng user thành UsersDto có đầy đủ thông tin Role, Permission
			var usersDto = users.Select(user =>
			{
				var userRoles = roles.Where(r => user.RoleIds.Contains(r.Id)).ToList();
				var userPermissionIds = userRoles.SelectMany(r => r.PermissionIds).Distinct().ToList();
				var userPermissions = permissions.Where(p => userPermissionIds.Contains(p.Id)).ToList();

				return new UsersDto
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

					RoleIds = userRoles.Select(r => r.Id).ToList(),
					RoleNames = userRoles.Select(r => r.RoleName ?? "").ToList(),

					PermissionIds = userPermissionIds,
					PermissionNames = userPermissions.Select(p => p.PermissionName ?? "").ToList(),
				};
			}).ToList();

			return new PagedResultDto<UsersDto>(pagingData.TotalCount, usersDto);
		}

		public async Task<UsersDto> SignIn(SignInInfo input)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Username == input.Username && a.Password == input.Password);

			if (user == null)
				return null;

			// Lấy role dựa trên RoleIds của user
			var roles = await _dbContext.Roles.Where(r => user.RoleIds.Contains(r.Id)).ToListAsync();

			// Lấy permission Ids từ role
			var permissionIds = roles.SelectMany(r => r.PermissionIds).Distinct().ToList();

			// Lấy permission
			var permissions = await _dbContext.Permissions.Where(p => permissionIds.Contains(p.Id)).ToListAsync();

			return new UsersDto
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

				RoleIds = roles.Select(r => r.Id).ToList(),
				RoleNames = roles.Select(r => r.RoleName ?? "").ToList(),

				PermissionIds = permissionIds,
				PermissionNames = permissions.Select(p => p.PermissionName ?? "").ToList(),
			};
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
			entity.Role = input.Role??Role.Admin;
			entity.RoleIds = input.RoleIds ?? new List<int>();
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
				RoleIds = entity.RoleIds,
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
                    Role = dto.Role,
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
