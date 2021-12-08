using Service.Domain.Teams.Models;
using Service.Domain.Teams.Services;

namespace Api.Controllers;

public class TeamsController : BaseController
{
    #region Fields

    private readonly TeamService _teamService;

    #endregion Fields

    #region Ctor

    public TeamsController(TeamService teamService)
    {
        _teamService = teamService;
    }

    #endregion Ctor

    #region Actions

    [HttpGet, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<IList<TeamListViewModel>>> GetAllTeams(CancellationToken cancellationToken)
    {
        var teamDtos = await _teamService.GetAllTeamsAsync(cancellationToken);
        return new ApiResponse<IList<TeamListViewModel>>(true, ApiResultBodyCode.Success, teamDtos);
    }

    [HttpGet("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<TeamViewModel>> GetTeamsById(int id, CancellationToken cancellationToken)
    {
        var teamDto = await _teamService.GetTeamsByIdAsync(id, cancellationToken);
        return new ApiResponse<TeamViewModel>(true, ApiResultBodyCode.Success, teamDto);
    }

    [HttpPost, Authorize(Roles = ApplicationRoles.TenantAdmin)]
    public async Task<ApiResponse> CreateTeam(TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _teamService.CreateAsync(teamCreateOrUpdateViewModel, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpPut("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse> UpdateTeam(int id, TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _teamService.UpdateAsync(id, teamCreateOrUpdateViewModel, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpDelete("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse> DeleteTeam(int id, CancellationToken cancellationToken)
    {
        await _teamService.DeleteAsync(id, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    #endregion Actions
}
