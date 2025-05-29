using Azure.Core;
using humanResourceManager.Datas;
using humanResourceManager.Datas.RoleAndPermission;
using humanResourceManager.Enums;
using humanResourceManager.IServices;
using humanResourceManager.Models;
using humanResourceManager.Models.ICurrentUser;
using humanResourceManager.Models.PositionsModel;
using humanResourceManager.Models.RoleModel;
using humanResourceManager.Ulity;
using Microsoft.EntityFrameworkCore;

namespace humanResourceManager.Services
{
	public class RoleService : IRoleService
	{
		private readonly MyDbContext _dbContext;
		private readonly ICurrentUserExtended _currentUser;

		public RoleService(MyDbContext dbContext, ICurrentUserExtended currentUser)
		{
			_dbContext = dbContext;
			_currentUser = currentUser;
		}
		public async Task<RoleDto> CreateAsync(CreateUpdateRoleDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement_Create))
			{
				throw new Exception("Không có quyền tạo mới vai trò");
			}

			Rolee entity = new Rolee
			{
				RoleName = input.RoleName,
				PermissionIds = input.PermissionIds ?? new List<int>(),
				CreationName = input.UserName,
				CreationTime = DateTime.Now
			};

			_dbContext.Roles.Add(entity);
			await _dbContext.SaveChangesAsync();

			// tìm kiếm các permision
			var permissions = _dbContext.Permissions.AsQueryable();
			var listNamePermission = permissions.Where(x => entity.PermissionIds.Contains(x.Id)).Select(p => p.PermissionName).ToList();

			return new RoleDto
			{
				Id = entity.Id,
				RoleName = entity.RoleName,
				PermissionIds = entity.PermissionIds,
				PermissionNames = listNamePermission,

				IsDeleted = entity.IsDeleted,
				IsStatic = entity.IsStatic,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<RoleDto> UpdateAsync(int id, CreateUpdateRoleDto input)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement_Update))
			{
				throw new Exception("Không có quyền cập nhật thông tin vai trò");
			}

			var entity = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new Exception("Không tìm thấy vai trò cần cập nhật");
			}

			entity.RoleName = input.RoleName;
			entity.PermissionIds = input.PermissionIds;
			entity.UpdatedBy = input.UserName;
			entity.UpdatedAt = DateTime.Now;

			_dbContext.Roles.Update(entity);
			await _dbContext.SaveChangesAsync();

			// tìm kiếm các permision
			var permissions = _dbContext.Permissions.AsQueryable();
			var listNamePermission = permissions.Where(x => entity.PermissionIds.Contains(x.Id)).Select(p => p.PermissionName).ToList();


			return new RoleDto
			{
				Id = entity.Id,
				RoleName = entity.RoleName,
				PermissionIds = entity.PermissionIds,
				PermissionNames = listNamePermission,

				IsDeleted = entity.IsDeleted,
				IsStatic = entity.IsStatic,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task DeleteAsync(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement_Delete))
			{
				throw new Exception("Không có quyền xoá vai trò");
			}

			var entity = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu vai trò này");
			}
			if (entity.IsStatic == true)
			{
				throw new Exception("Không thể xoá vai trò mặc định");
			}

			_dbContext.Roles.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteMultipleAsync(IEnumerable<int> ids)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement_Delete))
			{
				throw new Exception("Không có quyền xoá vai trò");
			}

			var entities = await _dbContext.Roles.Where(x => ids.Contains(x.Id)).ToListAsync();
			if (entities == null || entities.Count == 0)
			{
				throw new BusinessException("Không tìm thấy bản ghi nào");
			}

			foreach (var entity in entities)
			{
				if (entity.IsStatic == true)
				{
					throw new BusinessException($"Không thể xoá vai trò mặc định: {entity.RoleName}");
				}
			}

		    _dbContext.Roles.RemoveRange(entities);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<RoleDto> GetById(int id)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement))
			{
				throw new Exception("Không có quyền xem thông tin chi tiết vai trò");
			}

			var entity = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
			{
				throw new BusinessException("Không tìm thấy dữ liệu vai trò này");
			}

			// tìm kiếm các permision
			var permissions = _dbContext.Permissions.AsQueryable();
			var listNamePermission = permissions.Where(x => entity.PermissionIds.Contains(x.Id)).Select(p => p.PermissionName).ToList();


			return new RoleDto
			{
				Id = entity.Id,
				RoleName = entity.RoleName,
				PermissionIds = entity.PermissionIds,
				PermissionNames = listNamePermission,
				
				IsDeleted = entity.IsDeleted,
				IsStatic = entity.IsStatic,
				CreationName = entity.CreationName,
				CreationTime = entity.CreationTime,
				UpdatedBy = entity.UpdatedBy,
				UpdatedAt = entity.UpdatedAt,
			};
		}

		public async Task<PagedResultDto<RoleDto>> GetPagingDto(PagingRequest request)
		{
			if (!_currentUser.PermissionNames.Contains(Permissions.RoleManagement))
			{
				throw new Exception("Không có quyền xem danh sách vai trò");
			}

			var roles = _dbContext.Roles.AsQueryable();
			if (request.Keyword != null)
			{
				roles = roles.Where(x => x.RoleName.Trim().ToLower().Contains(request.Keyword.Trim().ToLower()));
			}

			var queryResult = roles
			.Select(a => new RoleDto()
			{
				Id = a.Id,
				RoleName = a.RoleName,
				PermissionIds = a.PermissionIds,
				PermissionNames = new List<string>(),

				IsDeleted = a.IsDeleted,
				IsStatic = a.IsStatic,
				CreationName = a.CreationName,
				CreationTime = a.CreationTime,
				UpdatedBy = a.UpdatedBy,
				UpdatedAt = a.UpdatedAt,
			});

			var pagingData = await PageResult<RoleDto>.PageAsync(queryResult, request.PageNumber - 1, request.PageSize, request.Field, request.FieldOption);


			var lstItems = pagingData.Items.ToList();

			// Bước 1: gom tất cả PermissionId đang được dùng
			var allPermissionIds = lstItems.SelectMany(x => x.PermissionIds).Distinct().ToList();

			// Bước 2: lấy toàn bộ permission từ DB 1 lần
			var permissionMap = await _dbContext.Permissions
				.Where(x => allPermissionIds.Contains(x.Id))
				.ToDictionaryAsync(x => x.Id, x => x.PermissionName);

			// Bước 3: gán PermissionNames theo permissionMap
			foreach (var item in lstItems)
			{
				item.PermissionNames = item.PermissionIds
					.Where(id => permissionMap.ContainsKey(id))
					.Select(id => permissionMap[id])
					.ToList();
			}
			return new PagedResultDto<RoleDto>(pagingData.TotalCount, lstItems);
		}



		public async Task<List<SelectOptionItems>> GetselectOptionRole()
		{
			var roles = _dbContext.Roles;

			var result = await roles
				.Select(a => new SelectOptionItems
				{
					Value = a.Id,
					Label = a.RoleName,
				})
				.ToListAsync();

			return result;
		}

		public async Task<List<SelectOptionItems>> GetselectOptionPermission()
		{
			var permissions = _dbContext.Permissions;

			var result = await permissions
				.Select(a => new SelectOptionItems
				{
					Value = a.Id,
					Label = a.PermissionName,
				})
				.ToListAsync();

			return result;
		}

		public async Task SeedDataOriginal()
		{
			//Logic thực hiện:
			//Tạo các quyền nếu chưa có.
            // Tạo Role Admin nếu chưa có.
            // Gán tất cả quyền vào role Admin.
            // Tạo người dùng admin nếu chưa có.
            // Gán Role Admin cho người dùng admin.

			var now = DateTime.Now;

			// Bước 1: Danh sách các quyền cần tạo
			var permissionNames = Permissions.All;

			// Bước 2: Kiểm tra và thêm Permission nếu chưa có
			var existingPermissions = await _dbContext.Permissions
				.Select(p => p.PermissionName)
				.ToListAsync();

			var newPermissions = permissionNames
				.Except(existingPermissions ?? new List<string>())
				.Select(name => new Permission
				{
					PermissionName = name,
					CreationTime = now
				}).ToList();

			if (newPermissions.Any())
			{
				await _dbContext.Permissions.AddRangeAsync(newPermissions);
				await _dbContext.SaveChangesAsync();
			}

			// Bước 3: Tìm hoặc tạo Role "Admin"
			var adminRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
			if (adminRole == null)
			{
				// Lấy lại tất cả permission sau khi thêm mới
				var allPermissions = await _dbContext.Permissions.ToListAsync();
				adminRole = new Rolee
				{
					RoleName = "Admin",
					PermissionIds = allPermissions.Select(p => p.Id).ToList(),
					IsStatic = true, // Đánh dấu là role mặc định
					CreationName = "System",
					CreationTime = now
				};
				await _dbContext.Roles.AddAsync(adminRole);
				await _dbContext.SaveChangesAsync();
			}
			else
			{
				// Nếu role đã có nhưng chưa đầy đủ quyền thì cập nhật thêm
				var allPermissionIds = await _dbContext.Permissions.Select(x => x.Id).ToListAsync();
				var missingPermissionIds = allPermissionIds.Except(adminRole.PermissionIds).ToList();
				if (missingPermissionIds.Any())
				{
					adminRole.PermissionIds.AddRange(missingPermissionIds);
					_dbContext.Roles.Update(adminRole);
					await _dbContext.SaveChangesAsync();
				}
			}

			// Bước 4: Tìm hoặc tạo User "admin"
			var adminUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == "admin");
			if (adminUser == null)
			{
				adminUser = new Users
				{
					Username = "admin",
					Password = "123456", // Nên mã hóa nếu môi trường thật
					RoleIds = new List<int> { adminRole.Id },
					IsStatic = true, // Đánh dấu là user mặc định
					CreationName = "System",
					CreationTime = now
				};
				await _dbContext.Users.AddAsync(adminUser);
				await _dbContext.SaveChangesAsync();
			}
			else
			{
				// Nếu user đã có nhưng thiếu role Admin thì bổ sung
				if (!adminUser.RoleIds.Contains(adminRole.Id))
				{
					adminUser.RoleIds.Add(adminRole.Id);
					_dbContext.Users.Update(adminUser);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
