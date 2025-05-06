namespace humanResourceManager.Models
{
	public class MessageResponseDto
	{
        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class MessageDto
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }
    }
}
