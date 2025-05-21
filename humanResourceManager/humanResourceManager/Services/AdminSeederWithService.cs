using humanResourceManager.IServices;
using humanResourceManager.Models.UsersModel;

namespace humanResourceManager.Services
{
	public static class AdminSeederWithService
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var userService = scope.ServiceProvider.GetRequiredService<IUsersService>();

			// Tạo DTO đăng ký tài khoản admin
			var dto = new RegisterDto
			{
				Username = "admin",
				Password = "admin123", // Mật khẩu có thể cho mạnh hơn
				EmployeeID = 0, // Nếu không gán cho nhân viên cụ thể

			};

			try
			{
				await userService.RegisterAsync(dto);
				Console.WriteLine("👑 Admin account seeded successfully.");
			}
			catch (Exception ex)
			{
				// Nếu tài khoản đã tồn tại thì không làm gì
				if (ex.Message.Contains("Username already exists"))
					Console.WriteLine("✔️ Admin account already exists.");
				else
					Console.WriteLine($"❌ Admin seeding failed: {ex.Message}");
			}
		}
	}

}
