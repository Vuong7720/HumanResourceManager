using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace humanResourceManager.Datas
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string? FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? Gender { get; set; }
        public int? PhoneNumber { get; set; }

        [MaxLength(128)]
        public string? Email { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }

        public int? PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Positions? Position { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Departments? Department { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? Salary { get; set; }

        public DateTime? HireDate { get; set; }
        public EmployeeStatus? Status { get; set; }
        public bool IsDeleted { get; set; } = false;

        [MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }

        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Collections (Optional – 1:N)
        public ICollection<Contracts>? Contracts { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Payroll>? Payrolls { get; set; }
        public ICollection<Users>? Users { get; set; }

    }

}
