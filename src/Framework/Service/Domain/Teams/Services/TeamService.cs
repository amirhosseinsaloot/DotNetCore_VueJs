using Data.DataProviders;
using Data.Entities;

namespace Services.Domain;

public class TeamService
{
    #region Fields

    private readonly IDataProvider<Team> _teamDataProvider;

    #endregion

    #region Ctor

    public TeamService(IDataProvider<Team> teamDataProvider)
    {
        _teamDataProvider = teamDataProvider;
    }

    #endregion

    #region Methods

    public async Task<IList<TeamListViewModel>> GetAllTeamsAsync(CancellationToken cancellationToken)
    {
        return await _teamDataProvider.GetAllAsync<TeamListViewModel>(cancellationToken);
    }

    public async Task<TeamViewModel> GetTeamsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _teamDataProvider.GetByIdAsync<TeamViewModel>(id, cancellationToken);
    }

    public async Task CreateAsync(TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _teamDataProvider.AddAsync(teamCreateOrUpdateViewModel, cancellationToken);
    }

    public async Task UpdateAsync(int id, TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _teamDataProvider.UpdateAsync(id, teamCreateOrUpdateViewModel, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _teamDataProvider.RemoveAsync(id, cancellationToken);
    }

    #endregion
}
