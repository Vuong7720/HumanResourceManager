using humanResourceManager.Datas;
using humanResourceManager.Enums;

namespace humanResourceManager.Models.EmployeesModel
{
	public class CreateUpdateEmployeesDto
	{
		public string? FullName { get; set; }
		public DateTime? BirthDay { get; set; }
		public int? Gender { get; set; }
		public int? PhoneNumber { get; set; }

		public string? Email { get; set; }
		public string? Address { get; set; }

		public int? PositionId { get; set; }

		//public Positions? Position { get; set; }

		public int? DepartmentId { get; set; }

		//public Departments? Department { get; set; }

		public Decimal? Salary { get; set; }

		public DateTime? HireDate { get; set; }
		public EmployeeStatus? Status { get; set; }

		public string? UserName { get; set; }
		public int UserId { get; set; }
		public string? ContractType { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
