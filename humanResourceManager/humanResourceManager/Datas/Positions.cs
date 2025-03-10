using System.ComponentModel.DataAnnotations;

namespace humanResourceManager.Datas
{
    public class Positions
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string? PositionName { get; set; }
    }
}
