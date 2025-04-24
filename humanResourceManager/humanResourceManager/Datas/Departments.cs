using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public bool IsDeleted { get; set; } = false;
        [MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }
        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
