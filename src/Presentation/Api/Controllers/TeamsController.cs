using Data.DataProviders;
using Data.Entities.Teams;
using Service.DomainDto.Team;

namespace Api.Controllers;

public class TeamsController : BaseController
{
    private readonly IDataProvider<Team> _teamDataProvider;

    public TeamsController(IDataProvider<Team> teamDataProvider)
    {
        _teamDataProvider = teamDataProvider;
    }

    [HttpGet, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<IList<TeamListDto>>> GetAllTeams(CancellationToken cancellationToken)
    {
        var teamDtos = await _teamDataProvider.GetAllAsync<TeamListDto>(cancellationToken);
        return new ApiResponse<IList<TeamListDto>>(true, ApiResultBodyCode.Success, teamDtos);
    }

    [HttpGet("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<TeamDto>> GetTeamsById(int id, CancellationToken cancellationToken)
    {
        var teamDto = await _teamDataProvider.GetByIdAsync<TeamDto>(id, cancellationToken);
        return new ApiResponse<TeamDto>(true, ApiResultBodyCode.Success, teamDto);
    }

    [HttpPost, Authorize(Roles = ApplicationRoles.TenantAdmin)]
    public async Task<ApiResponse> CreateTeam(TeamCreateUpdateDto teamCreateOrUpdateDto, CancellationToken cancellationToken)
    {
        await _teamDataProvider.AddAsync(teamCreateOrUpdateDto, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpPut("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse> UpdateTeam(int id, TeamCreateUpdateDto teamCreateOrUpdateDto, CancellationToken cancellationToken)
    {
        await _teamDataProvider.UpdateAsync(id, teamCreateOrUpdateDto, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpDelete("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse> DeleteTeam(int id, CancellationToken cancellationToken)
    {
        await _teamDataProvider.RemoveAsync(id, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }
}