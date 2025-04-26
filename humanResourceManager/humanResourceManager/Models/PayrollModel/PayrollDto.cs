using humanResourceManager.Datas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.PayrollModel
{
	public class PayrollDto
	{
		public int Id { get; set; }

		public int? EmployeeID { get; set; }
		public Employees? Employee { get; set; }

		public int? Month { get; set; }
		public int? Year { get; set; }

		public decimal? BaseSalary { get; set; }

		public decimal? Bonus { get; set; }

		public decimal? Deductions { get; set; }

		public decimal? NetSalary { get; set; }

		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
