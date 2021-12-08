using AutoMapper;
using Data.Entities.Identity;
using Service.Domain.Roles.Models;
using Service.Domain.Users.Models;

namespace Api.AutoMapperConfiguration.Profiles.Authentication;

public class AuthenticationProfiles : Profile
{
    #region Ctor

    public AuthenticationProfiles()
    {
        UserProfile();
        RoleProfile();
    }

    #endregion Ctor

    #region Methods

    public void UserProfile()
    {
        CreateMap<UserCreateViewModel, User>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.IsActive, src => src.MapFrom(s => true))
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.SecurityStamp, src => src.Ignore())
            .ForMember(dest => dest.AccessFailedCount, src => src.Ignore())
            .ForMember(dest => dest.LastLoginDate, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnabled, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnd, src => src.Ignore())
            .ForMember(dest => dest.NormalizedEmail, src => src.Ignore())
            .ForMember(dest => dest.EmailConfirmed, src => src.Ignore())
            .ForMember(dest => dest.NormalizedUserName, src => src.Ignore())
            .ForMember(dest => dest.PasswordHash, src => src.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, src => src.Ignore())
            .ForMember(dest => dest.RefreshToken, src => src.Ignore())
            .ForMember(dest => dest.RefreshTokenExpirationTime, src => src.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, src => src.Ignore());

        CreateMap<UserUpdateViewModel, User>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.SecurityStamp, src => src.Ignore())
            .ForMember(dest => dest.AccessFailedCount, src => src.Ignore())
            .ForMember(dest => dest.LastLoginDate, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnabled, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnd, src => src.Ignore())
            .ForMember(dest => dest.NormalizedEmail, src => src.Ignore())
            .ForMember(dest => dest.EmailConfirmed, src => src.Ignore())
            .ForMember(dest => dest.NormalizedUserName, src => src.Ignore())
            .ForMember(dest => dest.PasswordHash, src => src.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, src => src.Ignore())
            .ForMember(dest => dest.RefreshToken, src => src.Ignore())
            .ForMember(dest => dest.RefreshTokenExpirationTime, src => src.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, src => src.Ignore());

        CreateMap<User, UserViewModel>();

        CreateMap<User, UserListViewModel>();
    }

    public void RoleProfile()
    {
        CreateMap<RoleCreateUpdateViewModel, Role>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.NormalizedName, src => src.Ignore());

        CreateMap<Role, RoleViewModel>();

        CreateMap<Role, RoleListViewModel>();
    }

    #endregion Methods
}
