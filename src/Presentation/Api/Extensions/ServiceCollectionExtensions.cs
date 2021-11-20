﻿using Core.Enums;
using Core.Exceptions;
using Core.Setting;
using Data;
using Data.DataInitializer;
using Data.DataProviders;
using Data.DbObjects;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Domain;
using Services.Emails;
using Services.Files.Services;
using Services.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, DatabaseSetting databaseSetting)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (databaseSetting.DatabaseProvider == DatabaseProvider.SqlServer)
            {
                options
                    .UseSqlServer(databaseSetting.ConnectionStrings.SqlServer);
            }
            else
            {
                options
                    .UseNpgsql(databaseSetting.ConnectionStrings.Postgres);
            }
        });
    }

    public static void AddCustomIdentity(this IServiceCollection services, IdentitySetting settings)
    {
        services.AddIdentity<User, Role>(identityOptions =>
        {
            //Password Settings
            identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
            identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
            identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric; //#@!
            identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
            identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

            //UserName Settings
            identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void AutoMapperRegistration(this IServiceCollection services)
    {
        services.InitializeAutoMapper();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // Default : 5 min
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true, // Default : false
                ValidAudience = jwtSettings.Audience,

                ValidateIssuer = true, // Default : false
                ValidIssuer = jwtSettings.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is not null)
                    {
                        throw new TokenExpiredException();
                    }

                    return Task.CompletedTask;
                },

                OnTokenValidated = async context =>
                {
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    if (claimsIdentity.Claims?.Any() is not true)
                    {
                        context.Fail("This token has no claims.");
                    }

                    var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                    if (string.IsNullOrEmpty(securityStamp))
                    {
                        context.Fail("This token has no security stamp");
                    }

                    //Find user and token from database and perform your custom validation
                    var userId = claimsIdentity.GetUserId<int>();
                    var user = await userManager.FindByIdAsync(userId.ToString());

                    var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    if (validatedUser is null)
                    {
                        context.Fail("Token security stamp is not valid.");
                    }

                    if (user.IsActive is false)
                    {
                        context.Fail("User is not active.");
                    }
                },

                OnChallenge = context =>
                {
                    if (context.AuthenticateFailure is not null)
                    {
                        throw new AuthenticationException(additionalData: context.AuthenticateFailure);
                    }

                    throw new ForbiddenException();
                },
            };
        });
    }

    public static void AddApplicationDependencyRegistration(this IServiceCollection services, ApplicationSettings applicationSettings)
    {
        // Data Services
        AddDataProvidersDependencyRegistration(services);
        AddDbObjectsDependencyRegistration(services, applicationSettings);

        // Domain Services
        services.AddScoped(typeof(JwtService));
        services.AddScoped(typeof(IAuthService), typeof(AuthService));

        if (applicationSettings.DatabaseSetting.StoreFilesOnDatabase)
        {
            services.AddScoped(typeof(IFileService), typeof(FileOnDatabaseService));
        }
        else
        {
            services.AddScoped(typeof(IFileService), typeof(FileOnFileSystemService));
        }

        services.AddScoped(typeof(IEmailService), typeof(EmailService));
        services.AddScoped(typeof(IEmailsLogService), typeof(EmailsLogService));

        services.AddScoped(typeof(RoleService));
        services.AddScoped(typeof(UserService));
        services.AddScoped(typeof(TeamService));
    }

    private static void AddDataProvidersDependencyRegistration(IServiceCollection services)
    {
        services.AddScoped(typeof(IDataProvider<>), typeof(DataProvider<>));
        services.AddScoped(typeof(ITeamDataProvider), typeof(TeamDataProvider));
        services.AddScoped(typeof(ITenantDataProvider), typeof(TenantDataProvider));

        services.AddScoped(typeof(DataInitializer));
    }

    private static void AddDbObjectsDependencyRegistration(IServiceCollection services, ApplicationSettings applicationSettings)
    {
        if (applicationSettings.DatabaseSetting.DatabaseProvider == DatabaseProvider.Postgres)
        {
            services.AddScoped(typeof(ITeamDbObject), typeof(TeamDbObjectPostgres));
            services.AddScoped(typeof(ITenantDbObject), typeof(TenantDbObjectPostgres));
        }

        if (applicationSettings.DatabaseSetting.DatabaseProvider == DatabaseProvider.SqlServer)
        {
            services.AddScoped(typeof(ITeamDbObject), typeof(TeamDbObjectSqlServer));
            services.AddScoped(typeof(ITenantDbObject), typeof(TenantDbObjectSqlServer));
        }
    }
}
