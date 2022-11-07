using System;
using Microsoft.AspNetCore.Identity;

namespace Jausentest.Domain.Entities;

public class UserEntity : IdentityUser<string>
{
    public BeislEntity Beisl { get; set; }
    
    public UserEntity()
    {
        base.Id = Guid.NewGuid().ToString();
        base.SecurityStamp = Guid.NewGuid().ToString();
    }
    
}