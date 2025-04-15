using ECommerce.Application.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // Đây là một constraint cho generic type TRequest, Đảm bảo rằng chỉ các class (Command/Query) nào implement IRequest<TResponse> mới được phép truyền vào IPipelineBehavior để xử lý.
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators; // tự động lấy tất cả validator từ DI Container dựa trên TRequest (VD: CreateProductCommandValidator vì nó implement IValidator với kiểu TRequest là CreateProductCommand)

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) // ktra có validator nào không, nếu kh có thì kh cần ktra tính hợp lệ
                return await next(); // Cho phép request đi tiếp đến Handler chính(Command/ Query Handler).

            ValidationContext<TRequest> context = new(request); // chứa dữ liệu của request

            // lấy kết quả của từng validator lưu vào mảng (có thể hợp lệ hoặc kh)
            ValidationResult[] validationFailures = await Task.WhenAll( // WhenAll giúp chạy tất cả Validator song song, thay vì chạy từng cái 1, Tối ưu tốc độ kiểm tra dữ liệu đầu vào.
                _validators.Select(validator => validator.ValidateAsync(context)));  // Validate với từng validator có trong _validators

            // lấy danh sách lỗi
            List<ValidationError> errors = validationFailures
                .Where(validationResult => !validationResult.IsValid) // chỉ lấy những ValidateResult kh hợp lệ (isvalid = false)
                .SelectMany(validationResult => validationResult.Errors) // từ mỗi ValidateResult lấy ra ds lỗi ValidationFailure
                .Select(validationFailures => new ValidationError( // chuyển từng ValidationFailure thành ValidationError tự định nghĩa
                    validationFailures.PropertyName,
                    validationFailures.ErrorMessage))
                .ToList();

            // Ktra và xuất ra lỗi nếu có
            if (errors.Any())
                throw new ValidationAppException(errors);

            return await next();
        }
    }
}
