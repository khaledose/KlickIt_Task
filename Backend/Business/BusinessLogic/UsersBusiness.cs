using Business.ApiRequests.UserModels;
using Business.Exceptions;
using Business.IBusinessLogic;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Business.BusinessLogic;

public class UsersBusiness : IUsersBusiness
{
    private UserManager<User> _userManager { get; init; }
    private RoleManager<Role> _roleManager { get; init; }
    private IConfiguration _configuration { get; init; }

    public UsersBusiness(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<Dictionary<string, string>> Login(LoginModel request)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        var user = await _userManager.FindByNameAsync(request.Username);
        bool isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (user is null || !isValidPassword)
        {
            throw new HttpStatusException("Something wrong either with username or password",
                                            HttpStatusCode.Unauthorized);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var token = GetToken(authClaims);
        result["Token"] = new JwtSecurityTokenHandler().WriteToken(token);
        result["UserId"] = user.Id.ToString();
        result["Username"] = user.UserName;
        result["Role"] = userRoles.FirstOrDefault() ?? "";
        return result;
    }

    public async Task<bool> Register(RegisterModel request)
    {

        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists is not null)
        {
            throw new HttpStatusException("User Already Exists!", HttpStatusCode.BadRequest);
        }

        User user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await RegisterRole(user, request.Role);
        }
        return result.Succeeded;
    }

    public async Task<JwtSecurityToken> GetTokenById(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new HttpStatusException("Something wrong either with username or password",
                                            HttpStatusCode.Unauthorized);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return GetToken(authClaims);
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
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

    private async Task RegisterRole(User user, string role)
    {
        if(!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new Role(role));
        }

        await _userManager.AddToRoleAsync(user, role);    
    }
}