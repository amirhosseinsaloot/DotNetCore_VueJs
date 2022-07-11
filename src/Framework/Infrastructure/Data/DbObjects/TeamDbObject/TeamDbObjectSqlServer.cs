using Domain.Entities.Teams;

namespace Infrastructure.Data.DbObjects.TeamDbObject;

public class TeamDbObjectSqlServer : ITeamDbObject
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected readonly DbSet<Team> _dbSet;

    public TeamDbObjectSqlServer(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<Team>();
    }

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
}
