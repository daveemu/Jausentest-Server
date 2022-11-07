using System;
using Microsoft.AspNetCore.Identity;

namespace Jausentest.Domain.Entities;

public class UserRoleEntity : IdentityRole<string>
{
    public UserRoleEntity() : base()
    {
        
    }
    public UserRoleEntity(string roleName)
    {
        base.Id = Guid.NewGuid().ToString();
        base.Name = roleName;
    }
}