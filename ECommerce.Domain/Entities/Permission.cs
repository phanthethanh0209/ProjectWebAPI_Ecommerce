﻿namespace ECommerce.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
