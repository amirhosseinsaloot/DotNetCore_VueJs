using Data.Entities.Teams;

namespace Data.DataProviders.TeamDataProvider;

public interface ITeamDataProvider : IDataProvider<Team>
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
    /// Asynchronously get the child teams of given team in all levels that mapped to TDto.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="rootTeamId">Id of the rootTeam.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of Team entity
    /// that contains the child teams of given team in all levels that mapped to TDto.
    /// </returns>
    Task<IList<TDto>> GetChildTeamAsync<TDto>(int rootTeamId, CancellationToken cancellationToken) where TDto : class, IDtoList;

    /// <summary>
    /// Asynchronously get the root team of given team in highest level in hierarchy.
    /// </summary>
    /// <param name="childTeamId">Id of child team for finding its root team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is root Team entity of given team in highest level in hierarchy.
    /// </returns>
    Task<Team?> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously get the root team of given team in highest level in hierarchy that mapped to TDto.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="childTeamId">Id of child team for finding its root team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is root Team entity of given team in highest level in hierarchy that mapped to TDto.
    /// </returns>
    Task<TDto?> GetRootTeamAsync<TDto>(int childTeamId, CancellationToken cancellationToken) where TDto : class, IDto;

    /// <summary>
    /// Asynchronously find the root team from user team in highest level in hierarchy.
    /// </summary>
    /// <param name="userId">Id of user for finding root team in highest level in hierarchy from user team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is the root Team entity from user team in highest level in hierarchy.
    /// </returns>
    Task<Team?> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously find the root team user team in highest level in hierarchy that mapped to TDto.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="userId">Id of user for finding root team in highest level in hierarchy from user team.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result is the root Team entity from user team in highest level in hierarchy that mapped to TDto.
    /// </returns>
    Task<TDto?> GetRootTeamByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IDto;
}
