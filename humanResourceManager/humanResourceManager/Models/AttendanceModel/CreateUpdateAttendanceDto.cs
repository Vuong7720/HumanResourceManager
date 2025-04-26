using humanResourceManager.Datas;
using humanResourceManager.Enums;

namespace humanResourceManager.Models.AttendanceModel
{
	public class CreateUpdateAttendanceDto
	{
		public int? EmployeeID { get; set; }
		//public Employees? Employee { get; set; }

		public DateTime? Date { get; set; }
		public DateTime? CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public AttendanceStatus? Status { get; set; }

        public string? UserName { get; set; }
        public int UserId { get; set; }
    }
}
