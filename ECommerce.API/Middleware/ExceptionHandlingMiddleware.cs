using ECommerce.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // ghi log cho kiểu ExceptionHandlingMiddleware 

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // đc chạy đầu tiên khi có request gọi đến, rồi mới chạy đến PipilineBehavior của Mediator
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // chạy đến middleware tiếp theo
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
                ExceptionDetails exceptionDetails = GetExceptionDetails(exception);

                // gán exception và các thông tin lỗi vào cho problemDetails
                ProblemDetails problemDetails = new()
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail,
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status; // set trạng thái cho http để phản hồi
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            // switch để phân loại loại ngoại lệ nhận được. Nếu ngoại lệ là ValidationException,
            // một đối tượng ExceptionDetails mới sẽ được tạo với mã trạng thái 400, cùng thông tin liên quan
            // Nếu không, sẽ tạo một đối tượng ExceptionDetails với mã trạng thái 500
            return exception switch
            {
                ValidationAppException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validation error",
                    "One or more validation errors has occurred",
                    validationException.Errors),

                NotFoundException notFoundException => new ExceptionDetails(
                    StatusCodes.Status404NotFound,
                     "NotFound",
                    "Resource not found",
                    notFoundException.Message,
                    null),

                ForbiddenAccessException forbiddenException => new ExceptionDetails(
                    StatusCodes.Status403Forbidden,
                     "Forbidden",
                    "Access denied ",
                    forbiddenException.Message,
                    null),

                //_ => new ExceptionDetails(
                //    StatusCodes.Status500InternalServerError,
                //    "ServerError",
                //    "Server error",
                //    "An unexpected error has occurred",
                //    null)
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors);

    }
}
