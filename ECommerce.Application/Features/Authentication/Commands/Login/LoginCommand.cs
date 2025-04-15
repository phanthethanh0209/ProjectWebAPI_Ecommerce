using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Authentication.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    // dùng record giúp data kh bị thay đổi sau mỗi lần khởi tạo,
    // record ngắn gọn và tự sinh constructor, equals, toString, v.v.
    // hay vì dùng record có thể dùng
    // class
    //public class LoginCommand : IRequest<string>
    //{
    //    public string Username { get; init; }
    //    public string Password { get; init; }

    //    public LoginCommand(string username, string password)
    //    {
    //        Username = username;
    //        Password = password;
    //    }
    //}
    public record class LoginCommand(string email, string password) : IRequest<ResultResponse<LoginResponse>>;
}
