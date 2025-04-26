using humanResourceManager.Datas;

namespace humanResourceManager.Models.PayrollModel
{
	public class CreateUpdatePayrollDto
	{
		public int? EmployeeID { get; set; }
		//public Employees? Employee { get; set; }

		public int? Month { get; set; }
		public int? Year { get; set; }

		public decimal? BaseSalary { get; set; }

		public decimal? Bonus { get; set; }

		public decimal? Deductions { get; set; }

		public decimal? NetSalary { get; set; }

		public string? UserName { get; set; }
		public int UserId { get; set; }
	}
}
