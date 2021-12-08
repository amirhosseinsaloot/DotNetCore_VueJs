using Data.Entities.Teams;

namespace Data.DbObjects.TeamDbObject;

public interface ITeamDbObject
{
    /// <summary>
    /// Asynchronously get the child teams of given team in all levels.
    /// </summary>
    /// <param name="rootTeamId">Id of the rootTeam.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of Team entity
    /// that contains the child teams of given team in all levels.
    /// </returns>
    Task<IList<Team>> GetChildTeamAsync(int rootTeamId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously get the root team of given team in highest level in hierarchy.
    /// </summary>
    /// <param name="childTeamId">Id of child team for finding its root team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is root Team entity of given team in highest level in hierarchy.
    /// </returns>
    Task<Team> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously find the root team from user team in highest level in hierarchy.
    /// </summary>
    /// <param name="userId">Id of user for finding root team in highest level in hierarchy from user team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is the root Team entity from user team in highest level in hierarchy.
    /// </returns>
    Task<Team> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken);
}
