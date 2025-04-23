using ECommerce.Application.Common.Responses;
using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResultResponse<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<ResultResponse<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // check refresh token in DB
            RefreshToken storedToken = await _unitOfWork.RefreshToken.GetFirstOrDefaultAsync(t => t.Token == request.refreshToken);
            if (storedToken is null || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
            {
                return ResultResponse<LoginResponse>.FailResponse("Invalid refresh token");
            }

            // get userId from token
            User user = await _unitOfWork.User.GetFirstOrDefaultAsync(t => t.Id.Equals(storedToken.UserId));
            if (user is null)
            {
                return ResultResponse<LoginResponse>.FailResponse("User not found");
            }

            // update revoked for old token
            storedToken.IsRevoked = true;
            await _unitOfWork.RefreshToken.Update(storedToken);
            await _unitOfWork.SaveChangesAsync();

            // generate access and refresh token
            LoginResponse token = await _jwtProvider.GenerateToken(user);
            return ResultResponse<LoginResponse>.SuccessResponse(token);
        }
    }
}
