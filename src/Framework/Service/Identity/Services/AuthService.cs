using AutoMapper;
using Core.Exceptions;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Jwt;
using System.Security.Claims;

namespace Services.Domain;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IMapper _mapper;

    private readonly JwtService _jwtService;

    private readonly UserManager<User> _userManager;

    private readonly SignInManager<User> _signInManager;

    #endregion Fields

    #region Ctor

    public AuthService(IMapper mapper, JwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #endregion Ctor

    #region Methods

    public async Task<UserSignInViewModel> SignInAsync(TokenRequest tokenRequest)
    {
        if (tokenRequest.GrantType.Equals("password", StringComparison.OrdinalIgnoreCase) is false)
        {
            throw new BadRequestException("Grant type is not valid.");
        }

        var user = await _userManager.FindByNameAsync(tokenRequest.Username ?? string.Empty);
        if (user is null)
        {
            throw new NotFoundException("Username or Password is incorrect.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var signInResult = await _signInManager.PasswordSignInAsync(user, tokenRequest.Password, true, true);

        if (signInResult.IsLockedOut)
        {
            throw new BadRequestException("User is lockedOut");
        }

        else if (signInResult.IsNotAllowed)
        {
            throw new ForbiddenException("User is not allowed");
        }

        else
        {
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, tokenRequest.Password);
            if (isPasswordValid is false)
            {
                throw new NotFoundException("Username or Password is incorrect.");
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.Roles = roles;

            var token = await _jwtService.GenerateTokenAsync(user);

            var userSignInViewModel = new UserSignInViewModel
            {
                UserViewModel = userViewModel,
                Token = token
            };

            // Update User
            user.LastLoginDate = DateTime.UtcNow;
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;

            await _userManager.UpdateAsync(user);

            return userSignInViewModel;
        }
    }

    public async Task<UserSignInViewModel> RegisterAsync(UserCreateViewModel userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.LastLoginDate = DateTime.UtcNow;

        // Create User
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Succeeded is false)
        {
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);
        }

        // Create Token
        var token = await _jwtService.GenerateTokenAsync(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;

        // Add RefreshToken to User entity
        await _userManager.UpdateAsync(user);

        var userViewModel = _mapper.Map<UserViewModel>(user);

        var userSignInViewModel = new UserSignInViewModel
        {
            UserViewModel = userViewModel,
            Token = token
        };

        return userSignInViewModel;
    }

    public async Task LogoutAsync(ClaimsPrincipal claimsPrincipal)
    {
        await _signInManager.SignOutAsync();

        var user = await _userManager.FindByNameAsync(claimsPrincipal.Identity.Name);
        user.RefreshToken = null;
        user.RefreshTokenExpirationTime = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task<Token> RefreshTokenAsync(TokenRequest tokenRequest)
    {
        if (!tokenRequest.GrantType.Equals("refresh_token", StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException("Invalid client request.");
        }

        if (tokenRequest is null)
        {
            throw new BadRequestException("Invalid client request.");
        }
        var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
        var username = principal.Identity.Name; //this is mapped to the Name claim by default

        var user = await _userManager.FindByNameAsync(username ?? string.Empty);
        if (user == null || user.RefreshToken != tokenRequest.RefreshToken)
        {
            throw new BadRequestException("Invalid client request.");
        }

        if (user.RefreshTokenExpirationTime <= DateTime.UtcNow)
        {
            throw new TokenExpiredException("Refresh token expired.");
        }
        var token = await _jwtService.GenerateTokenAsync(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;
        await _userManager.UpdateAsync(user);

        return token;
    }

    #endregion Methods
}
