using AutoMapper;
using Data.DbObjects.TeamDbObject;
using Data.Entities.Teams;

namespace Data.DataProviders.TeamDataProvider;

public class TeamDataProvider : DataProvider<Team>, ITeamDataProvider
{
    #region Fields

    private readonly ITeamDbObject _teamDbObject;

    #endregion

    #region Ctor

    public TeamDataProvider(ApplicationDbContext dbContext, IMapper mapper, ITeamDbObject teamDbObject) : base(dbContext, mapper)
    {
        _teamDbObject = teamDbObject;
    }

    #endregion Ctor

    #region Methods

    public async Task<IList<Team>> GetChildTeamAsync(int rootTeamId, CancellationToken cancellationToken)
    {
        return await _teamDbObject.GetChildTeamAsync(rootTeamId, cancellationToken);
    }

    public async Task<IList<TDto>> GetChildTeamAsync<TDto>(int rootTeamId, CancellationToken cancellationToken) where TDto : class, IDtoList
    {
        var entities = await _teamDbObject.GetChildTeamAsync(rootTeamId, cancellationToken);
        return _mapper.Map<IList<TDto>>(entities);
    }

    public async Task<Team?> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken)
    {
        return await _teamDbObject.GetRootTeamAsync(childTeamId, cancellationToken);
    }

    public async Task<TDto?> GetRootTeamAsync<TDto>(int childTeamId, CancellationToken cancellationToken) where TDto : class, IDto
    {
        var entity = await _teamDbObject.GetRootTeamAsync(childTeamId, cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<Team?> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await _teamDbObject.GetRootTeamByUserAsync(userId, cancellationToken);
    }


    public async Task<TDto?> GetRootTeamByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IDto
    {
        var entity = await _teamDbObject.GetRootTeamByUserAsync(userId, cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    #endregion Methods
}
