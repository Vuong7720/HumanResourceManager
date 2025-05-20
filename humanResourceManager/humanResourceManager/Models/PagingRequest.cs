namespace humanResourceManager.Models
{
	public class PagingRequest
	{
		public string Field { get; set; } = "id";

		public bool FieldOption { get; set; } = true;

		public int PageSize { get; set; } = 10;

		public int PageNumber { get; set; } = 1;

		public string? Keyword { get; set; }

		public DateTime? FilterDate { get; set; } // lọc theo ngày cụ thể
		public TimeSpan? LateAfter { get; set; }  // ví dụ: 08:00 sáng
		public TimeSpan? LeaveAfter { get; set; } // ví dụ: 19:00 tối
	}
}
