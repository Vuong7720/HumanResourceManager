using humanResourceManager.Datas;
using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.EmployeesModel
{
	public class EmployeesDto
	{
		public int Id { get; set; }

		public string? FullName { get; set; }
		public DateTime? BirthDay { get; set; }
		public int? Gender { get; set; }
		public string? PhoneNumber { get; set; }

		public string? Email { get; set; }
		public string? Address { get; set; }

		public int? PositionId { get; set; }

		public Positions? Position { get; set; }

		public int? DepartmentId { get; set; }

		public Departments? Department { get; set; }
		public string? DepartmentName { get; set; }
		public string? PositionName { get; set; }

        public Decimal? Salary { get; set; }

		public DateTime? HireDate { get; set; }
		public EmployeeStatus? Status { get; set; }
		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Navigation Collections (Optional – 1:N)
		public ICollection<Contracts>? Contracts { get; set; }
		public ICollection<Attendance>? Attendances { get; set; }
		public ICollection<Payroll>? Payrolls { get; set; }
		public ICollection<Users>? Users { get; set; }

	}
}
