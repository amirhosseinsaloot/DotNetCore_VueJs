using Core.Entities.Identity;
using Core.Entities.Teams;
using Core.StaticData;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.DataInitializer;

public class DataInitializer
{
    private readonly UserManager<User> _userManager;

    private readonly RoleManager<Role> _roleManager;

    private readonly ApplicationDbContext _dbContext;

    public DataInitializer(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public void InstallRequierdData()
    {
        InsertTenants();
        InsertTeams();
        InsertRoles();
        InsertUsers();
        InsertUserRoles();
    }

    private void InsertTenants()
    {
        var tenants = new List<Tenant>
                {
                    new Tenant {
                        Name = "Manhattan Tenant"
                    },
                    new Tenant {
                        Name = "England Tenant"
                    },
                };

        foreach (var tenant in tenants)
        {
            if (_dbContext.Set<Tenant>().AsNoTracking().Any(p => p.Name == tenant.Name) is false)
            {
                _dbContext.Add(tenant);
            }
        }

        _dbContext.SaveChanges();
    }

    private void InsertTeams()
    {
        var teams = new List<Team>
                {
                    new Team {
                        Name = "Manhattan Team",
                        Description = "Manhattan Team of Health",
                        ChildTeams = new List<Team> {
                            new Team { Name = "Broadway Team", Description = "Broadway Team of Health" },
                            new Team { Name = "Canal Team", Description = "Canal Team of Health" },
                        },
                        TenantId = _dbContext.Set<Tenant>().FirstOrDefault(p=>p.Name == "Manhattan Tenant")?.Id,
                    },
                    new Team {
                        Name = "England Team",
                        Description = "England Team",
                        ChildTeams = new List<Team> {
                            new Team { Name = "London Team", Description = "London Team" },
                            new Team { Name = "Manchester Team", Description = "Manchester Team" , ChildTeams = new List<Team> {
                                                                                                 new Team { Name = "Cheetham Hill Road Team", Description = "Cheetham Hill Road Team" },
                                                                                                 new Team { Name = "Deansgate Team", Description = "Deansgate Team" },
                                                                                                 new Team { Name = "Corporation Street Team", Description = "Corporation Street Team" },
                                                                                                 new Team { Name = "King Street Team", Description = "King Street Team" , ChildTeams = new List<Team> {
                                                                                                                                                                         new Team { Name = "100 King Street Team", Description = "100 King Street Team" },
                                                                                                                                                                         new Team { Name = "Canal House Team", Description = "Canal House Team" },
                                                                                                                                                                         new Team { Name = "Reform Club Team", Description = "Reform Club Team" },
                                                                                                                                                                         }}
                                                                                                 }},
                        },
                        TenantId = _dbContext.Set<Tenant>().FirstOrDefault(p=>p.Name == "England Tenant")?.Id,
                    }
               };

        foreach (var team in teams)
        {
            if (_dbContext.Set<Team>().AsNoTracking().Any(p => p.Name == team.Name) is false)
            {
                _dbContext.Add(team);
            }
        }

        _dbContext.SaveChanges();
    }

    private void InsertRoles()
    {
        var roles = new List<Role>
                {
                    new Role { Name = ApplicationRoles.TenantAdmin , Description = "This role has superuser privileges for managing all teams in tenant" },
                    new Role { Name = ApplicationRoles.TeamAdmin , Description = "This role used to team admins for manage team members" },
                    new Role { Name = ApplicationRoles.TeamMember, Description = "This role belong to users that work in Teams" },
                };

        foreach (var role in roles)
        {
            if (_roleManager.Roles.AsNoTracking().Any(p => p.Name == role.Name) is false)
            {
                _roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
        }
    }

    private void InsertUsers()
    {
        var manhattanTeam = _dbContext.Set<Team>().FirstOrDefault(p => p.Name == "Manhattan Team");
        int teamId;
        if (manhattanTeam is null)
        {
            throw new Exception("Error occured in seed sample data.");
        }

        teamId = manhattanTeam.Id;

        var users = new List<User>
                {
                    new User { Firstname = "TenantAdmin" , Lastname = "New York" , Gender = GenderType.Male , UserName = "TenantAdmin" , Email = "TenantAdmin@site.com" ,
                               TeamId = teamId , Birthdate = DateTime.UtcNow },

                    new User { Firstname = "TeamAdmin" , Lastname = "Manhattan Team" , Gender = GenderType.Male , UserName = "TeamAdmin" , Email = "TeamAdmin@site.com" ,
                               TeamId = teamId , Birthdate = DateTime.UtcNow },

                    new User { Firstname = "TeamMember1" , Lastname = "Manhattan Team" , Gender = GenderType.Male , UserName = "TeamMember1" , Email = "TeamMember1@site.com" ,
                               TeamId = teamId , Birthdate = DateTime.UtcNow },

                    new User { Firstname = "TeamMember2" , Lastname = "Manhattan Team" , Gender = GenderType.Male , UserName = "TeamMember2" , Email = "TeamMember2@site.com" ,
                               TeamId = teamId , Birthdate = DateTime.UtcNow},
                };

        foreach (var user in users)
        {
            if (_userManager.Users.AsNoTracking().Any(p => p.UserName == user.UserName) is false)
            {
                _userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();
            }
        }
    }

    private void InsertUserRoles()
    {
        var userRoles = new List<UserRole>
                {
                   new UserRole { UserId = _userManager.FindByNameAsync("TenantAdmin").Result.Id , RoleId = _roleManager.FindByNameAsync(ApplicationRoles.TenantAdmin).Result.Id },
                   new UserRole { UserId = _userManager.FindByNameAsync("TeamAdmin").Result.Id , RoleId = _roleManager.FindByNameAsync(ApplicationRoles.TeamAdmin).Result.Id },
                   new UserRole { UserId = _userManager.FindByNameAsync("TeamMember1").Result.Id , RoleId = _roleManager.FindByNameAsync(ApplicationRoles.TeamMember).Result.Id },
                   new UserRole { UserId = _userManager.FindByNameAsync("TeamMember2").Result.Id , RoleId = _roleManager.FindByNameAsync(ApplicationRoles.TeamMember).Result.Id },
                };

        foreach (var userRole in userRoles)
        {
            if (_dbContext.Set<UserRole>().AsNoTracking().Any(p => p.UserId == userRole.UserId && p.RoleId == userRole.RoleId) is false)
            {
                _dbContext.Add(userRole);
            }
        }

        _dbContext.SaveChanges();
    }
}
