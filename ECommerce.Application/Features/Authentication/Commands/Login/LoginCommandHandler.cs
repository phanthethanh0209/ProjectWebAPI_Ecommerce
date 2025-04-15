using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultResponse<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<ResultResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // check password
            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Email == request.email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.password, user.Password))
            {
                // dính cấu hình ở global nên ra lỗi 500 (sẽ sửa)
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // generate access and refresh token
            LoginResponse token = await _jwtProvider.GenerateToken(user);
            return ResultResponse<LoginResponse>.SuccessResponse(token);
        }
    }
}
