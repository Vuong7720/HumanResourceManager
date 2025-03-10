using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public AttendanceStatus? Status { get; set; }
    }
}
