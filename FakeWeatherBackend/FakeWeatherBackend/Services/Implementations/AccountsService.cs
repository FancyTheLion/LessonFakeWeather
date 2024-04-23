using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FakeWeatherBackend.DAO.Models.Authentification;
using FakeWeatherBackend.Models.API.DTOs.Users;
using FakeWeatherBackend.Models.API.Enums;
using FakeWeatherBackend.Models.Settings;
using FakeWeatherBackend.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FakeWeatherBackend.Services.Implementations;

public class AccountsService : IAccountsService
{
    private readonly UserManager<UserDbo> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AccountsService
    (
        UserManager<UserDbo> userManager,
        IOptions<JwtSettings> jwtSettings
    )
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<RegistrationResultDto> RegisterUserAsync(RegistrationRequestDto request)
    {
        _ = request ?? throw new ArgumentNullException(nameof(request), "Registration request mustn't be null!");

        if (await _userManager.FindByNameAsync(request.Login) != null)
        {
            return new RegistrationResultDto(Guid.Empty, RegistrationResult.LoginIsTaken);
        }
        
        var userToRegister = new UserDbo()
        {  
            UserName = request.Login,
            SecurityStamp = Guid.NewGuid().ToString() // TODO: Is it secure?
        };
        
        var result = await _userManager.CreateAsync(userToRegister, request.Password);

        if (!result.Succeeded)
        {
            return new RegistrationResultDto(Guid.Empty, RegistrationResult.GenericError);  
        }
        
        return new RegistrationResultDto(userToRegister.Id, RegistrationResult.OK);
    }

    public async Task<LoginResultDto> LoginAsync(LoginDto loginData)
    {
        _ = loginData ?? throw new ArgumentNullException(nameof(loginData), "Login data must not be null.");
        
        var user = await _userManager.FindByNameAsync(loginData.Login);
        if (user == null)
        {
            // User not found
            return new LoginResultDto(false, string.Empty, DateTime.UtcNow);
        }

        if (!await _userManager.CheckPasswordAsync(user, loginData.Password))
        {
            // Password incorrect
            return new LoginResultDto(false, string.Empty, DateTime.UtcNow);
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
  
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));  
  
        var token = new JwtSecurityToken(
            _jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.Lifetime),  
            claims: authClaims,  
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
        );

        return new LoginResultDto(true, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }

    public async Task<bool> IsUserExistByLoginAsync(string login)
    {
        return await _userManager.FindByNameAsync(login) != null;
    }
}