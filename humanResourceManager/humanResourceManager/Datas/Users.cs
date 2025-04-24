using humanResourceManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        [MaxLength(128)]
        public string Username { get; set; }
        [MaxLength(128)]
        public string Password { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; } = false;
        [MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }
        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
