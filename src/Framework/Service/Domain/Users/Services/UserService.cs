using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Service.Domain.Users.Models;

namespace Service.Domain.Users.Services;

public class UserService
{
    #region Fields

    private readonly IMapper _mapper;

    private readonly UserManager<User> _userManager;

    #endregion Fields

    #region Ctor

    public UserService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    #endregion Ctor

    #region Methods

    public async Task<List<UserListViewModel>> GetAllUsersAsync()
    {
        return await _userManager.Users
            .ProjectTo<UserListViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserViewModel> GetByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        await _userManager.UpdateSecurityStampAsync(user);

        var userViewModel = _mapper.Map<UserViewModel>(user);
        return userViewModel;
    }

    public async Task<UserViewModel> CreateAsync(UserCreateViewModel userCreateViewModel)
    {
        var doesExists = await _userManager.Users.AnyAsync(p => p.UserName == userCreateViewModel.Username);
        if (doesExists is true)
        {
            throw new DuplicateException("This user already exists");
        }

        var user = _mapper.Map<User>(userCreateViewModel);
        var identityResult = await _userManager.CreateAsync(user, userCreateViewModel.Password);
        if (identityResult.Succeeded is false)
        {
            throw new Exception("Server error");
        }

        var userViewModel = _mapper.Map<UserViewModel>(user);
        return userViewModel;
    }

    public async Task UpdateAsync(int id, UserUpdateViewModel userUpdateViewModel)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(userUpdateViewModel, user);

        await _userManager.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new NotFoundException();
        }

        await _userManager.DeleteAsync(user);
    }

    #endregion Methods
}
