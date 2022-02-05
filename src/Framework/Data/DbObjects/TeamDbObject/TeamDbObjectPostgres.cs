using Data.Entities.Teams;

namespace Data.DbObjects.TeamDbObject;

public class TeamDbObjectPostgres : ITeamDbObject
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected readonly DbSet<Team> _dbSet;

    public TeamDbObjectPostgres(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<Team>();
    }

    public async Task<IList<Team>> GetChildTeamAsync(int rootTeamId, CancellationToken cancellationToken)
    {
        return await _dbSet
                     .FromSqlRaw($"SELECT id , name , description , parent_id , tenant_id , created_on FROM get_child_teams({rootTeamId})")
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
    }

    public async Task<Team?> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                           .FromSqlRaw($"SELECT id , name , description , parent_id , tenant_id , created_on FROM get_root_team({childTeamId})")
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);

        return result.FirstOrDefault();
    }

    public async Task<Team?> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _dbSet
                           .FromSqlRaw($"SELECT id , name , description , parent_id , tenant_id , created_on FROM get_root_team_by_user({userId})")
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);
        return result.FirstOrDefault();
    }
}
