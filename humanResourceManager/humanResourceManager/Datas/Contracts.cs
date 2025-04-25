using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace humanResourceManager.Datas
{
    public class Contracts
    {
        [Key]
        public int Id { get; set; }

        public int? EmployeeID { get; set; }
        public Employees? Employee { get; set; }

        [MaxLength(128)]
        public string? ContractType { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? Salary { get; set; }

        public bool IsDeleted { get; set; } = false;

        [MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }

        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
