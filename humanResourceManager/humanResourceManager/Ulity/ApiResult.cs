namespace humanResourceManager.Ulity
{
	public interface IApiResult
	{
		bool Status { get; set; }

		string Message { get; set; }

		int? Code { get; set; }
	}
	public class ApiResult : IApiResult
	{
		public bool Status { get; set; } = true;


		public string? Message { get; set; }

		public int? Code { get; set; }

		public object? Data { get; set; }

		public int? TotalRecord { get; set; }

		public static ApiResult Error(string message = "")
		{
			return new ApiResult
			{
				Message = message,
				Status = false
			};
		}

		public static ApiResult Success(object data, int totalRecord = 0, string message = "", int code = 0)
		{
			return new ApiResult
			{
				Data = data,
				TotalRecord = totalRecord,
				Message = message
			};
		}
		public static ApiResult Success(object data, string message = "")
		{
			return new ApiResult
			{
				Data = data,
				Message = message
			};
		}
	}
}
