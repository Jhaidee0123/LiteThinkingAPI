using LiteThinking.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiteThinking.Infrastructure.Authentication;

public class UserAuthenticationRepository : IUserAuthenticationRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserAuthenticationRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<(string, DateTime)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            throw new InvalidOperationException("Usuario o contraseña incorrecta");

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }

    public async Task RegisterAdminAsync(string email, string password)
    {
        var existingAdmin = await _userManager.FindByNameAsync(email);

        if (existingAdmin != null)
        {
            throw new InvalidOperationException("El usuario ya existe");
        }

        var admin = new User
        {
            UserName = email,
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(admin, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("No se pudo crear el usuario");
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (!await _roleManager.RoleExistsAsync(UserRoles.External))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.External));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
    }

    public async Task RegisterExternalAsync(string email, string password)
    {
        var existingExternal = await _userManager.FindByNameAsync(email);

        if (existingExternal != null)
        {
            throw new InvalidOperationException("El usuario ya existe");
        }

        var external = new User
        {
            UserName = email,
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = await _userManager.CreateAsync(external, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("No se pudo crear el usuario");
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (!await _roleManager.RoleExistsAsync(UserRoles.External))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.External));

        if (await _roleManager.RoleExistsAsync(UserRoles.External))
            await _userManager.AddToRoleAsync(external, UserRoles.External);
    }
}
