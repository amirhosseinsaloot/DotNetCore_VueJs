using Api.Dtos.AuthToken;
using Api.Dtos.User;
using AutoMapper;
using Core.Entities.Identity;
using Core.Exceptions;
using Core.Interfaces.DataProviders;
using Core.Interfaces.Services;
using Core.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Api.Controllers;

public class AuthorizationController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IAuthTokenService _tokenService;
    private readonly ITenantDataProvider _tenantDataProvider;
    private readonly ApplicationSettings _applicationSettings;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthorizationController(
        IMapper mapper,
        IAuthTokenService tokenService,
        ITenantDataProvider tenantDataProvider,
        IOptions<ApplicationSettings> settings,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _applicationSettings = settings.Value;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _tenantDataProvider = tenantDataProvider;
    }


    [HttpPost("Login"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInDto>> Login(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        if (tokenRequest.GrantType.Equals("password", StringComparison.OrdinalIgnoreCase) is false)
        {
            throw new BadRequestException("Grant type is not valid.");
        }

        var user = await _userManager.FindByNameAsync(tokenRequest.Username);
        if (user is null)
        {
            throw new NotFoundException("Username or Password is incorrect.");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, tokenRequest.Password, true, true);

        if (signInResult.IsLockedOut)
        {
            throw new BadRequestException("User is lockedOut");
        }

        if (signInResult.IsNotAllowed)
        {
            throw new ForbiddenException("User is not allowed");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, tokenRequest.Password);
        if (isPasswordValid is false)
        {
            throw new NotFoundException("username or password is incorrect.");
        }

        // Add custom claims
        var claims = await CreateClaimsAsync(user, cancellationToken);

        // Create token
        var token = CreateToken(claims);

        var userDto = _mapper.Map<UserDto>(user);
        var userSignInDto = new UserSignInDto
        {
            UserDto = userDto,
            Token = token
        };

        // Update User
        user.LastLoginDate = DateTime.UtcNow;
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;
        await _userManager.UpdateAsync(user);

        return new ApiResponse<UserSignInDto>(true, ApiResultBodyCode.Success, userSignInDto);
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInDto>> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(userCreateDto.Username) is not null)
        {
            throw new DuplicateException("This user already exists.");
        }

        var user = _mapper.Map<User>(userCreateDto);

        // Create User
        var result = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (result.Succeeded is false)
        {
            throw new Exception(result.Errors.FirstOrDefault()?.Description ?? "Can not register this user.");
        }

        // Add custom claims
        var claims = await CreateClaimsAsync(user, cancellationToken);

        // Create token
        var token = CreateToken(claims);

        var userDto = _mapper.Map<UserDto>(user);
        var userSignInDto = new UserSignInDto
        {
            UserDto = userDto,
            Token = token
        };

        // Update User
        user.LastLoginDate = DateTime.UtcNow;
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;
        await _userManager.UpdateAsync(user);

        return new ApiResponse<UserSignInDto>(true, ApiResultBodyCode.Success, userSignInDto);
    }

    [HttpPost("RefreshToken"), AllowAnonymous]
    public async Task<ApiResponse<Token>> RefreshToken(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        if (!tokenRequest.GrantType.Equals("refresh_token", StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException("Invalid client request.");
        }

        if (tokenRequest.AccessToken is null)
        {
            throw new BadRequestException("Invalid client request (AccessToken can not be empty).");
        }

        var principal = _tokenService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var username = principal.Identity?.Name; //this is mapped to the Name claim by default
        if (username is null)
        {
            throw new BadRequestException("Invalid client request.");
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null || user.RefreshToken != tokenRequest.RefreshToken)
        {
            throw new BadRequestException("Invalid client request.");
        }

        if (user.RefreshTokenExpirationTime <= DateTime.UtcNow)
        {
            throw new TokenExpiredException("Refresh token expired.");
        }

        // Create Token
        var token = CreateToken(principal.Claims);

        // Update User
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;
        await _userManager.UpdateAsync(user);

        return new ApiResponse<Token>(true, ApiResultBodyCode.Success, token);
    }

    private async Task<IEnumerable<Claim>> CreateClaimsAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _signInManager.ClaimsFactory.CreateAsync(user);
        var role = await _signInManager.UserManager.GetRolesAsync(user);
        var tenant = await _tenantDataProvider.GetTenantByUserAsync(user.Id, cancellationToken);

        // Add custom claims
        var claims = new List<Claim>(result.Claims)
            {
                new Claim(nameof(CurrentUser.Id), user.Id.ToString()),
                new Claim(nameof(CurrentUser.Username), user.UserName),
                new Claim(nameof(CurrentUser.Firstname), user.Firstname),
                new Claim(nameof(CurrentUser.Lastname), user.Lastname),
                new Claim(nameof(CurrentUser.Email), user.Email),
                new Claim(nameof(CurrentUser.Birthdate), user.Birthdate.ToString()),
                new Claim(nameof(CurrentUser.PhoneNumber), user.PhoneNumber ?? ""),
                new Claim(nameof(CurrentUser.Gender), user.Gender.ToString()),
                new Claim(nameof(CurrentUser.Roles), string.Join(",",role)),
                new Claim(nameof(CurrentUser.TeamId), user.TeamId.ToString()),
                new Claim(nameof(CurrentUser.TenantId), tenant!.Id.ToString()),
            };

        return claims;
    }

    private Token CreateToken(IEnumerable<Claim> claims)
    {
        return new Token
        {
            AccessToken = _tokenService.GenerateAccessToken(claims),
            RefreshToken = _tokenService.GenerateRefreshToken(),
            RefreshTokenExpiresIn = DateTime.UtcNow.AddDays(_applicationSettings.JwtSetting.RefreshTokenExpirationDays),
            TokenType = "Bearer"
        };
    }
}