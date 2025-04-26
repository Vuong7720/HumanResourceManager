using humanResourceManager.Datas;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Models.PositionsModel
{
	public class PositionsDto
	{
		public int Id { get; set; }

		public string? PositionName { get; set; }

		public bool IsDeleted { get; set; } = false;

		public string? CreationName { get; set; }
		public DateTime? CreationTime { get; set; }

		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Navigation property (1:N)
		public ICollection<Employees>? Employees { get; set; }
	}
}
