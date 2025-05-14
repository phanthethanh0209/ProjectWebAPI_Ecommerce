namespace ECommerce.Application.Interfaces.Authentication
{
    public interface ICurrentUserService
    {
        Guid GetUserIdForClaims();
    }
}
