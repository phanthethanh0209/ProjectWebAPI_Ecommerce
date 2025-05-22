namespace ECommerce.Domain.Entities
{
    public class RolePermission
    {
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
