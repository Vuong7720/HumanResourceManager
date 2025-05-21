using humanResourceManager.Datas;
using humanResourceManager.IServices;
using humanResourceManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using humanResourceManager.Models.UsersModel;
using humanResourceManager.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new() { Title = "HumanResource API", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header.  
Nhập 'Bearer [space] token' vào ô bên dưới.  
VD: Bearer abc123xyz",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = Microsoft.OpenApi.Models.ParameterLocation.Header
			},
			new List<string>()
		}
	});
});

builder.Services.AddDbContext<MyDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
});

// Đăng ký Service (bạn thêm dòng này)
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IPositionsService, PositionsService>();
builder.Services.AddScoped<IUsersService, UsersService>();


builder.Services.AddSingleton<JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = "yourdomain.com",
			ValidAudience = "yourdomain.com",
			IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
		};
	});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		policy =>
		{
			policy.WithOrigins("http://localhost:4200") // Angular dev server
				  .AllowAnyHeader()
				  .AllowAnyMethod()
				  .AllowCredentials(); // Nếu cần gửi cookie
		});
});


var app = builder.Build();
//await AdminSeederWithService.SeedAsync(app.Services);

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<MyDbContext>();

	var adminUsername = "admin";
	var adminPassword = "admin";

	if (!context.Users.Any(u => u.Username == adminUsername))
	{
		var admin = new Users
		{
			Username = adminUsername,
			Password = BCrypt.Net.BCrypt.HashPassword(adminPassword),
			Role = Role.Admin,
			CreationTime = DateTime.Now,
		};

		context.Users.Add(admin);
		await context.SaveChangesAsync();

		Console.WriteLine("🌟 Tài khoản admin đã được tạo thành công.");
	}
	else
	{
		Console.WriteLine("✅ Tài khoản admin đã tồn tại.");
	}
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");


app.UseHttpsRedirection();

app.UseAuthentication(); // Phải có nếu muốn xác thực JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
