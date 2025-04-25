using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Positions
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string? PositionName { get; set; }

        public bool IsDeleted { get; set; } = false;

        [MaxLength(128)]
        public string? CreationName { get; set; }
        public DateTime? CreationTime { get; set; }

        [MaxLength(128)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property (1:N)
        public ICollection<Employees>? Employees { get; set; }
    }

}
