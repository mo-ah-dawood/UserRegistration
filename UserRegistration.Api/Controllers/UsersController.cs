using Microsoft.AspNetCore.Mvc;
using UserRegistration.Api.Requests;
using UserRegistration.Api.Response;
using UserRegistration.Core.Services;

namespace UserRegistration.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterNewUser request)
    {
        var user = await _userService.RegisterUserAsync(request.ToUser());
        return Ok(GenericResult.Success(user.ToDto()));

    }

    [HttpPost]
    public async Task<IActionResult> VerifyPhone([FromBody] VerifyRequest request)
    {
        var user = await _userService.VerifyPhoneAsync(request.ICNumber, request.VerificationCode);
        return Ok(GenericResult.Success(user.ToDto()));

    }

    [HttpPost]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyRequest request)
    {
        var user = await _userService.VerifyEmailAsync(request.ICNumber, request.VerificationCode);
        return Ok(GenericResult.Success(user.ToDto()));

    }

    [HttpPost]
    public async Task<IActionResult> AcceptPrivacy(string userId)
    {
        var user = await _userService.AcceptPrivacy(userId);
        return Ok(GenericResult.Success(user.ToDto()));

    }

    [HttpPost]
    public async Task<IActionResult> SetPinCode([FromBody] CreatePinRequest request)
    {
        var user = await _userService.CreatePinAsync(request.UserId, request.PinCode);
        return Ok(GenericResult.Success(user.ToDto()));

    }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody()] LoginRequest request)
    {
        var user = await _userService.Login(request.ICNumber);
        return Ok(GenericResult.Success(user.ToLoginDto()));

    }

}


