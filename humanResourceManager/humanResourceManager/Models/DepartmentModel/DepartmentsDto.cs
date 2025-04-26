using humanResourceManager.Datas;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.DepartmentModel
{
	public class DepartmentsDto
	{
		public int Id { get; set; }
		public string? DepartmentName { get; set; }
		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Navigation property (1:N)
		public ICollection<Employees>? Employees { get; set; }
	}
}
