using FakeWeatherBackend.Models.API.DTOs.Users;
using FakeWeatherBackend.Models.API.Enums;
using FakeWeatherBackend.Models.API.Requests.Users;
using FakeWeatherBackend.Models.API.Responses.Users;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherBackend.Controllers;

/// <summary>
/// Controller to work with users
/// </summary>
[Authorize]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountsService;

    public AccountsController
    (
        IAccountsService accountsService
    )
    {
        _accountsService = accountsService;
    }

    /// <summary>
    /// Call this method to register an user
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [Route("api/Users/Register")]
    public async Task<ActionResult<RegistrationResultResponse>> AddWeatherAsync(
        [FromBody] RegistrationRequest registrationRequest)
    {
        if (registrationRequest == null)
        {
            return BadRequest("Registration request must not be null!");
        }

        if (registrationRequest.Request == null)
        {
            return BadRequest("User data must be populated!");
        }

        return Ok
        (
            new RegistrationResultResponse
            (
                await _accountsService.RegisterUserAsync(registrationRequest.Request)
            )
        );
    }

    /// <summary>
    /// Call this method to get JWT token
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [Route("api/Users/Login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        if (request == null)
        {
            return BadRequest("Login request mustn't be null!");
        }

        if (request.LoginData == null)
        {
            return BadRequest("Login data must not be null.");
        }
        
        var result = await _accountsService.LoginAsync(request.LoginData);
        
        return Ok(new LoginResponse(result));
    }
    
    /// <summary>
    /// Check if login is taken
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [Route("api/Users/IsLoginTaken")]
    public async Task<ActionResult<CheckIfLoginTakenResponse>> IsLoginTakenAsync([FromBody] CheckIfLoginTakenRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (request.CheckData == null)
        {
            return BadRequest("Check data must not be null.");
        }

        var isTaken = await _accountsService.IsUserExistByLoginAsync(request.CheckData.Login);

        return Ok(new CheckIfLoginTakenResponse(new CheckIfLoginTakenResultDto(isTaken)));
    }

}