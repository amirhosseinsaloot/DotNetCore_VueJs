using Core.Interfaces;
using System.Linq.Expressions;

namespace Data.DataProviders;

public interface IDataProvider<TEntity> where TEntity : IBaseEntity
{
    #region Get

    /// <summary>
    /// Asynchronously return all of entity records that mapped to TDto.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of TDto 
    /// that contains all of entity records that mapped to TDto.
    /// </returns>
    Task<IList<TDto>> GetAllAsync<TDto>(CancellationToken cancellationToken) where TDto : class, IListViewModel;

    /// <summary>
    /// Return all of entity records that mapped to TDto .
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <returns>Result contains a IList of TDto
    /// that contains all of entity records that mapped to TDto.
    /// </returns>
    IList<TDto> GetAll<TDto>() where TDto : class, IListViewModel;

    /// <summary>
    /// Asynchronously return all of entity records.
    /// </summary>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of TDto
    /// that contains all of entity records.
    /// </returns>
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Return all of entity records.
    /// </summary>
    /// <returns>Result contains a IList of TDto
    /// that contains all of entity records.
    /// </returns>
    IList<TEntity> GetAll();

    /// <summary>
    /// Asynchronously filters a sequence of values based on a predicate.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>
    ///  A task that represents the asynchronous operation. 
    ///  The task result contains a IList of TDto that filters a sequence of values based on a predicate that mapped to TDto.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    Task<IList<TDto>> GetListByConditionAsync<TDto>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TDto : class, IListViewModel;

    /// <summary>
    /// Filters a sequence of values based on a predicate
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity</typeparam>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <returns>The entity found that mapped to TDto.</returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    IList<TDto> GetListByCondition<TDto>(Expression<Func<TEntity, bool>> expression) where TDto : class, IListViewModel;

    /// <summary>
    /// Asynchronously filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///  A task that represents the asynchronous operation. 
    ///  The task result contains a IList of TEntity that filters a sequence of values based on a predicate that mapped to TEntity.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    Task<IList<TEntity>> GetListByConditionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <returns>
    ///  Result contains a IList<TEntity> that filters a sequence of values based on a predicate that mapped to TEntity.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    IList<TEntity> GetListByCondition(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains entity found that its type is TDto.
    /// </returns>
    Task<TDto> GetByIdAsync<TDto>(int id, CancellationToken cancellationToken) where TDto : class, IViewModel;

    /// <summary>
    /// Finds an entity with the given primary key values.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <returns>Result contains a TDto
    /// that contains entity found that mapped to TDto.
    /// </returns>
    TDto GetById<TDto>(int id) where TDto : class, IViewModel;

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains entity found that its type is TEntity.
    /// </returns>
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <returns>Result contains a TEntity
    /// that contains entity found.
    /// </returns>
    TEntity GetById(int id);

    /// <summary>
    /// Asynchronously find the first element of a sequence based on a predicate.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <param name="isFirstOrDefault">
    /// Determine result be first or last record that it will be found by conditions.
    /// If true then return first record and vice versa.
    /// Defalut value is True.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a TDto that contains
    /// the entity found that mapped to TDto.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    Task<TDto> GetByConditionAsync<TDto>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken, bool isFirstOrDefault = true) where TDto : class, IViewModel;

    /// <summary>
    /// Find the first element of a sequence based on a predicate.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="isFirstOrDefault">
    /// Determine result be first or last record that it will be found by conditions.
    /// If true then return first record and vice versa.
    /// Defalut value is True.
    /// </param>
    /// <returns>Result contains a TDto that contains
    /// the entity found that mapped to TDto.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    TDto GetByCondition<TDto>(Expression<Func<TEntity, bool>> expression, bool isFirstOrDefault = true) where TDto : class, IViewModel;

    /// <summary>
    /// Asynchronously find the first element of a sequence based on a predicate.
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <param name="isFirstOrDefault">
    /// Determine result be first or last record that it will be found by conditions.
    /// If true then return first record and vice versa.
    /// Defalut value is True.
    /// </param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a TEntity that contains The entity found that mapped to TDto.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken, bool isFirstOrDefault = true);

    /// <summary>
    /// Find the first element of a sequence based on a predicate
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="isFirstOrDefault">
    /// Determine result be first or last record that it will be found by conditions.
    /// If true then return first record and vice versa.
    /// Defalut value is True.
    /// </param>
    /// <returns>The entity found that mapped to TDto.</returns>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    TEntity GetByCondition(Expression<Func<TEntity, bool>> expression, bool isFirstOrDefault = true);

    #endregion Get

    #region Add

    /// <summary>
    /// Asynchronously adds given model that its type is TDto to database
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModel">The TDto to add.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a integer named Id of entity that have been added to database.
    /// </returns>
    Task<int> AddAsync<TDto>(TDto createUpdateViewModel, CancellationToken cancellationToken) where TDto : class, ICreateViewModel;

    /// <summary>
    /// Adds given model that its type is TDto to database
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModel">The TDto to add.</param>
    /// <returns>
    /// Result contains a integer named Id of entity that have been added to database.
    /// </returns>
    int Add<TDto>(TDto createUpdateViewModel) where TDto : class, ICreateViewModel;

    /// <summary>
    /// Asynchronously adds given entity that its type is TEntity to database.
    /// </summary>
    /// <param name="entity">The entity to add that its type is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// Id of entity that have been added to database.
    /// </returns>
    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Adds given entity that its type is TEntity to database.
    /// </summary>
    /// <param name="entity">The entity to add that its type is TEntity.</param>
    /// <returns>Id of entity that have been added to database.
    /// </returns>
    int Add(TEntity entity);

    /// <summary>
    /// Asynchronously adds given models that their type is TDto to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModels">Entities to add that their types is TDto.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    Task AddRangeAsync<TDto>(IEnumerable<TDto> createUpdateViewModels, CancellationToken cancellationToken) where TDto : class, ICreateViewModel;

    /// <summary>
    /// Adds given models that their type is TDto to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModels">Entities to add that their types is TDto.</param>
    void AddRange<TDto>(IEnumerable<TDto> createUpdateViewModels) where TDto : class, ICreateViewModel;

    /// <summary>
    /// Asynchronously adds given models that their type is TEntity to database.
    /// </summary> 
    /// <param name="entities">Entities to add that their types is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of integers that contains Id's of entities that have been inserted to database.
    /// </returns>
    Task<List<int>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    /// <summary>
    /// Adds given models that their type is TEntity to database.
    /// </summary> 
    /// <param name="entities">Entities to add that their types is TEntity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    List<int> AddRange(IEnumerable<TEntity> entities);

    #endregion Add

    #region Update

    /// <summary>
    /// Asynchronously updates given model that its type is TDto with given Id of TEntity to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="createUpdateViewModel">The entity to update that type is TDto.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.</returns>
    Task UpdateAsync<TDto>(int id, TDto createUpdateViewModel, CancellationToken cancellationToken) where TDto : class, IUpdateViewModel;

    /// <summary>
    /// Updates given model that its type is TDto with given Id of TEntity to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="createUpdateViewModel">The entity to update that type is TDto.</param>
    void Update<TDto>(int id, TDto createUpdateViewModel) where TDto : class, IUpdateViewModel;

    /// <summary>
    /// Asynchronously updates given model that its type is TEntity with given Id of TEntity to database.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="entity">The TEntity to update.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param> 
    /// <returns>
    /// A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates given model that its type is TEntity with given Id of TEntity to database.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="entity">The TEntity to update.</param>
    void Update(int id, TEntity entity);

    /// <summary>
    /// Asynchronously updates given models that their type is TDto to database.
    /// </summary>
    /// <param name="entities">IEnumerable of TEntity to add.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    /// <summary>
    /// Updates given models that their type is TDto to database.
    /// </summary>
    /// <param name="entities">IEnumerable of TEntity to update.</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    #endregion Update

    #region Remove

    /// <summary>
    /// Asynchronously removes entity with given Id from database.
    /// </summary>
    /// <param name="id">Id of entity that its type is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously removes entity with given Id from database.
    /// </summary>
    /// <param name="id">Id of entity that its type is TEntity.</param>
    void Remove(int id);

    #endregion Remove
}
