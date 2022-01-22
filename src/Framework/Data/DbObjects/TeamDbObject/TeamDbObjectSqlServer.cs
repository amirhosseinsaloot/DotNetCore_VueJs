using Data.Entities.Teams;

namespace Data.DbObjects.TeamDbObject;

public class TeamDbObjectSqlServer : ITeamDbObject
{
    #region Fields

    private readonly ApplicationDbContext _applicationDbContext;

    protected readonly DbSet<Team> _dbSet;

    #endregion Fields

    #region Ctor

    public TeamDbObjectSqlServer(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<Team>();
    }

    #endregion Ctor

    #region Methods

    public async Task<IList<Team>> GetChildTeamAsync(int rootTeamId, CancellationToken cancellationToken)
    {
        return await _dbSet
                     .FromSqlRaw($"EXEC GetChildTeams @RootTeamId = {rootTeamId}")
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
    }

    public async Task<Team?> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                           .FromSqlRaw($"EXEC GetRootTeam @ChildTeamId = {childTeamId}")
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);

        return result.FirstOrDefault();
    }

    public async Task<Team?> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                           .FromSqlRaw($"EXEC GetRootTeamByUser @InputUserId = {userId}")
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);
        return result.FirstOrDefault();
    }

    #endregion Methods
}
