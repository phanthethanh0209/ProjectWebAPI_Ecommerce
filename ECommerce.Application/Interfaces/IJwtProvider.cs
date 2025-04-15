using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        Task<LoginResponse> GenerateToken(User user);
    }
}
