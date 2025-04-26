using humanResourceManager.Datas;
using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.AttendanceModel
{
	public class AttendanceDto
	{
		public int Id { get; set; }

		// FK đến Employee
		public int? EmployeeID { get; set; }
		public Employees? Employee { get; set; }

		public DateTime? Date { get; set; }
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public AttendanceStatus? Status { get; set; }

		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
