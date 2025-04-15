namespace ECommerce.Application.Common.Responses
{
    public class ResultResponse<T>
    {

        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        //public ResultResponse(bool isSuccess, T data = default, string errorMessage = null)
        //{
        //    IsSuccess = isSuccess;
        //    Data = isSuccess ? data : default;
        //    ErrorMessage = isSuccess ? null : errorMessage;
        //}
        public static ResultResponse<T> SuccessResponse(T data)
        {
            return new ResultResponse<T> { IsSuccess = true, Data = data };
        }

        public static ResultResponse<T> FailResponse(string errorMessage)
        {
            return new ResultResponse<T> { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}
