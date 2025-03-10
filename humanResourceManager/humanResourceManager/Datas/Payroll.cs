using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace humanResourceManager.Datas
{
    public class Payroll
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? BaseSalary { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Bonus { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Deductions { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? NetSalary { get; set; }
    }
}
