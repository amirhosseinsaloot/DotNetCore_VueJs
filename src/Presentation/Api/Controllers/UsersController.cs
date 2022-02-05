using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities.Identity;
using Core.Exceptions;
using Infrastructure.Dto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class UsersController : BaseController
{
    private readonly IMapper _mapper;

    private readonly UserManager<User> _userManager;

    public UsersController(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<List<UserListDto>>> GetAllUsers()
    {
        var userList = await _userManager.Users
                             .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
        return new ApiResponse<List<UserListDto>>(true, ApiResultBodyCode.Success, userList);
    }

    [HttpGet("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<UserDto>> GetUserById(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        await _userManager.UpdateSecurityStampAsync(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new ApiResponse<UserDto>(true, ApiResultBodyCode.Success, userDto);
    }

    [HttpPost, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<UserDto>> CreateUser(UserCreateDto userCreateDto)
    {
        var doesExists = await _userManager.Users.AnyAsync(p => p.UserName == userCreateDto.Username);
        if (doesExists is true)
        {
            throw new DuplicateException("This user already exists");
        }

        var user = _mapper.Map<User>(userCreateDto);
        var identityResult = await _userManager.CreateAsync(user, userCreateDto.Password);
        if (identityResult.Succeeded is false)
        {
            throw new Exception("Server error");
        }

        var userDto = _mapper.Map<UserDto>(user);
        return new ApiResponse<UserDto>(true, ApiResultBodyCode.Success, userDto);
    }

    [HttpPut("{id:int:min(1)}"), Authorize]
    public async Task<ApiResponse> UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(userUpdateDto, user);

        await _userManager.UpdateAsync(user);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpDelete("{id:int:min(1)}"), Authorize]
    public async Task<ApiResponse> DeleteUser(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        await _userManager.DeleteAsync(user);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }
}