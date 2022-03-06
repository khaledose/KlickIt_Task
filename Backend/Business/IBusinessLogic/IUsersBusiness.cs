using Business.ApiRequests.UserModels;
using System.IdentityModel.Tokens.Jwt;

namespace Business.IBusinessLogic;

public interface IUsersBusiness
{
    public Task<Dictionary<string, string>> Login(LoginModel request);
    public Task<bool> Register(RegisterModel request);
    public Task<JwtSecurityToken> GetTokenById(string email);
}
