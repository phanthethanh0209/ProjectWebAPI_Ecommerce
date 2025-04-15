namespace ECommerce.Application.Common.Exceptions
{
    public record ValidationError(string property, string errorMessage);

    public class ValidationAppException : Exception
    {
        // IReadOnlyCollection<T> nghĩa là một collection chỉ đọc (không cho phép thêm, xóa, sửa phần tử bên trong),
        // dùng để đảm bảo rằng bên ngoài không thể thay đổi danh sách sau khi được tạo.
        public IReadOnlyCollection<ValidationError> Errors { get; }

        public ValidationAppException(IReadOnlyCollection<ValidationError> errors)
            : base("Validation Failed") // gọi class cha Exception để gán Message mặc định của exception sẽ là "Validation Failed".
        {
            Errors = errors; // gán lỗi tìm đc vào Errors của Exception để ném lỗi throw new exception, nhằm cho try catch đc bắt ở global middleware excpetion
        }
    }
}
