using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jausentest.Core.Config;
using Jausentest.Core.Models;
using Jausentest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jausentest.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{

    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<UserRoleEntity> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<UserEntity> userManager, RoleManager<UserRoleEntity> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _userManager.FindByNameAsync(login.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Register([FromBody] UserDto user)
    {
        var existingUser = await _userManager.FindByNameAsync(user.Username);
        if (existingUser != null)
            return Conflict();

        UserEntity _user = new()
        {
            Email = user.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = user.Username
        };
        var result = await _userManager.CreateAsync(_user, user.Password);
        if (!result.Succeeded)
            return Conflict();
        
        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new UserRoleEntity(UserRoles.Admin));
        
        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new UserRoleEntity(UserRoles.User));

        await _userManager.AddToRoleAsync(_user, UserRoles.User);
        await _userManager.AddToRoleAsync(_user, UserRoles.Admin);

        return Ok(new
        {
            Status = "Success",
            Message = "User created successfully"
        });
    }


    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}