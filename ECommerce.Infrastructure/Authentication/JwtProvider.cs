using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;

        public JwtProvider(IOptions<JwtOptions> jwtOptions, IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public string GenerateAccessToken(User user)
        {
            Claim[] claims = new Claim[] {
                new(JwtRegisteredClaimNames.Sub,  user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email,  user.Email),
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            SigningCredentials sigingCredentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                _jwtOptions.Audience,
                _jwtOptions.Issuer,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                sigingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomBytes = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<LoginResponse> GenerateToken(User user)
        {
            string accessToken = GenerateAccessToken(user);
            string refreshToken = await GenerateAndSaveRefreshTokenAsync(user);

            return new LoginResponse(accessToken, refreshToken);
        }

        public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            string token = GenerateRefreshToken();
            RefreshToken refreshToken = new()
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(1),
                IsRevoked = false,
                UserId = user.Id
            };

            // Save RefreshToken
            await _unitOfWork.RefreshToken.AddAsync(refreshToken);
            await _unitOfWork.SaveChangeAsync();

            return token;
        }
    }
}
