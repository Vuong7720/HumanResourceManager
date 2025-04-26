namespace humanResourceManager.Models
{
	public class PagingRequest
	{
		public string Field { get; set; } = "id";

		public bool FieldOption { get; set; } = true;

		public int PageSize { get; set; } = 10;

		public int PageNumber { get; set; } = 1;

		public string? Keyword { get; set; }
	}
}
