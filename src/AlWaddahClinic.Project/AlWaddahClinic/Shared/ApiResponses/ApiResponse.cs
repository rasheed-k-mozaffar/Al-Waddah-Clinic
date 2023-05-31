using AlWaddahClinic.Shared.Dtos;

namespace AlWaddahClinic.Shared.ApiResponses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Value { get; set; }
    }
}
