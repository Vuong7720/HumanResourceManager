using humanResourceManager.Datas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.ContractsModel
{
	public class ContractsDto
	{
		public int Id { get; set; }

		public int? EmployeeID { get; set; }
		public Employees? Employee { get; set; }

		public string? ContractType { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public Decimal? Salary { get; set; }

		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
