using API.Responses;
using Business.ApiRequests.UserModels;
using Business.Exceptions;
using Business.IBusinessLogic;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private IUsersBusiness _usersBusiness { get; init; }
    private ILogger<UsersController> _logger { get; init; }
    public UsersController(IUsersBusiness usersBusiness, ILogger<UsersController> logger)
    {
        _usersBusiness = usersBusiness;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<Response<User>> Login([FromBody] LoginModel model)
    {
        LoginResponse<User> response = new LoginResponse<User>();
        try
        {
            Dictionary<string, string> result = await _usersBusiness.Login(model);
            response.SetSuccess();
            response.Token = result["Token"];
            response.User = new UserResponse
            {
                UserId = result["UserId"],
                Username = result["Username"],
                Roles = new List<string> { result["Role"] }
            };
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }

    
    [HttpPost("register")]
    public async Task<Response<User>> Register([FromBody] RegisterModel model)
    {
        Response<User> response = new Response<User>();
        try
        {
            bool isSuccess = await _usersBusiness.Register(model);
            if (!isSuccess)
            {
                throw new Exception("Failed to register user");
            }
            response.SetSuccess();
        }
        catch (HttpStatusException ex)
        {
            response.SetFailure(ex.Message, ex.StatusCode);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex)
        {
            response.SetFailure(ex.Message, HttpStatusCode.InternalServerError);
            _logger.LogError(ex.Message, ex);
        }
        return response;
    }
}
