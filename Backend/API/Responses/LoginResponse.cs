using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace API.Responses;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class LoginResponse<T> : Response<T>
{
    public string? Token { get; set; }
    public UserResponse? User { get; set; }
}

public class UserResponse
{
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}