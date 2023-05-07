using System;
namespace AlWaddahClinic.Shared.ApiResponses
{
	public class ApiErrorResponse
	{
		public string? Message { get; set; }
		public List<string>? Errors { get; set; }
	}
}


	