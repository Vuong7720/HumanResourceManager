using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
    }
}
