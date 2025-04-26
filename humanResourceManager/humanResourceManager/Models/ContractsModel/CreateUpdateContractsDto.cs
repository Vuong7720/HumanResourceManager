using humanResourceManager.Datas;

namespace humanResourceManager.Models.ContractsModel
{
	public class CreateUpdateContractsDto
	{
		public int? EmployeeID { get; set; }
		//public Employees? Employee { get; set; }

		public string? ContractType { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public Decimal? Salary { get; set; }

		public string? UserName { get; set; }
		public int UserId { get; set; }
	}
}
