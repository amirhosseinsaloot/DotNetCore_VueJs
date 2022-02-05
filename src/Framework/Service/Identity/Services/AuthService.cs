using AutoMapper;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Service.DomainDto.User;
using Service.Identity.Models;
using Service.Jwt.Models;
using Service.Jwt.Services;

namespace Service.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;

    private readonly JwtService _jwtService;

    private readonly UserManager<User> _userManager;

    private readonly SignInManager<User> _signInManager;

    public AuthService(IMapper mapper, JwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserSignInDto> SignInAsync(TokenRequest tokenRequest, CancellationToken cancellationToken)
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

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles;

            var token = await _jwtService.GenerateTokenAsync(user, cancellationToken);

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

            return userSignInDto;
        }
    }

    public async Task<UserSignInDto> RegisterAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(userCreateDto.Username) is not null)
        {
            throw new DuplicateException("This user already exists.");
        }

        var user = _mapper.Map<User>(userCreateDto);
        user.LastLoginDate = DateTime.UtcNow;

        // Create User
        var result = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (result.Succeeded is false)
        {
            throw new Exception(result.Errors.FirstOrDefault()?.Description ?? "Can not register this user.");
        }

        // Create Token
        var token = await _jwtService.GenerateTokenAsync(user, cancellationToken);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;

        // Add RefreshToken to User entity
        await _userManager.UpdateAsync(user);

        var userDto = _mapper.Map<UserDto>(user);

        var userSignInDto = new UserSignInDto
        {
            UserDto = userDto,
            Token = token
        };

        return userSignInDto;
    }

    public async Task<Token> RefreshTokenAsync(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        if (!tokenRequest.GrantType.Equals("refresh_token", StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException("Invalid client request.");
        }

        if (tokenRequest.AccessToken is null)
        {
            throw new BadRequestException("Invalid client request (AccessToken can not be empty).");
        }

        var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
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
        var token = await _jwtService.GenerateTokenAsync(user, cancellationToken);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationTime = token.RefreshTokenExpiresIn;
        await _userManager.UpdateAsync(user);

        return token;
    }
}