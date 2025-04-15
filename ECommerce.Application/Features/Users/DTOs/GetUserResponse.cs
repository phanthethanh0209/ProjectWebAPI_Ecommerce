namespace ECommerce.Application.Features.Users.DTOs
{
    public record class GetUserResponse(Guid id, string name, string email, string phone);
}
