using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Service.Domain.Roles.Models;

namespace Service.Domain.Roles.Services;

public class RoleService
{
    #region Fields

    private readonly IMapper _mapper;

    private readonly RoleManager<Role> _roleManager;

    #endregion Fields

    #region Ctor

    public RoleService(IMapper mapper, RoleManager<Role> roleManager)
    {
        _mapper = mapper;
        _roleManager = roleManager;
    }

    #endregion Ctor

    #region Methods

    public async Task<List<RoleListViewModel>> GetAllRolesAsync()
    {
        return await _roleManager.Roles
            .ProjectTo<RoleListViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<RoleViewModel> GetRoleByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            throw new NotFoundException();
        }

        var roleDto = _mapper.Map<RoleViewModel>(role);

        return roleDto;
    }

    public async Task<RoleViewModel> CreateAsync(RoleCreateUpdateViewModel roleCreateOrUpdateViewModel)
    {
        var doesExists = await _roleManager.Roles.AnyAsync(p => p.Name == roleCreateOrUpdateViewModel.Name);
        if (doesExists is true)
        {
            throw new DuplicateException("This role already exists");
        }
        var role = _mapper.Map<Role>(roleCreateOrUpdateViewModel);
        await _roleManager.CreateAsync(role);

        var roleViewModel = _mapper.Map<RoleViewModel>(role);

        return roleViewModel;
    }

    public async Task UpdateAsync(int id, RoleCreateUpdateViewModel roleCreateOrUpdateViewModel)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(roleCreateOrUpdateViewModel, role);

        await _roleManager.UpdateAsync(role);
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            throw new NotFoundException();
        }

        await _roleManager.DeleteAsync(role);
    }

    #endregion Methods
}
