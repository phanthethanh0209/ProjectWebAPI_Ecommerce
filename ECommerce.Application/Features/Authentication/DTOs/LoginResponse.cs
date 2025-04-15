namespace ECommerce.Application.Features.Authentication.DTOs
{
    //public class LoginResponse
    //{
    //    public string AccessToken { get; set; }
    //    public string RefreshToken { get; set; }
    //}

    public record LoginResponse(string AccessToken, string RefreshToken);
}
