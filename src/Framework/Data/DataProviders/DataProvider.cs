using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.DataProviders;

public class DataProvider<TEntity> : IDataProvider<TEntity> where TEntity : class, IBaseEntity
{
    #region Fields

    private readonly ApplicationDbContext _dbContext;

    protected readonly DbSet<TEntity> _dbSet;

    protected readonly IMapper _mapper;

    #endregion Fields

    #region Ctor

    public DataProvider(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
        _mapper = mapper;
    }

    #endregion Ctor

    #region Methods

    #region Get

    /// <summary>
    /// Asynchronously return all of entity records that mapped to TDto .
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of TDto
    /// that contains all of entity records that mapped to TDto.
    /// </returns>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<IList<TDto>> GetAllAsync<TDto>(CancellationToken cancellationToken) where TDto : class, IListViewModel
    {
        return await _dbSet
                     .AsNoTracking()
                     .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Return all of entity records that mapped to TDto .
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <returns>Result contains a IList of TDto
    /// that contains all of entity records that mapped to TDto.
    /// </returns>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public IList<TDto> GetAll<TDto>() where TDto : class, IListViewModel
    {
        return _dbSet
               .AsNoTracking()
               .ProjectTo<TDto>(_mapper.ConfigurationProvider)
               .ToList();
    }

    /// <summary>
    /// Asynchronously return all of entity records.
    /// </summary>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of TDto
    /// that contains all of entity records.
    /// </returns>
    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Return all of entity records.
    /// </summary>
    /// <returns>Result contains a IList.<TDto>
    /// that contains all of entity records.
    /// </returns>
    public IList<TEntity> GetAll()
    {
        return _dbSet
               .AsNoTracking()
               .ToList();
    }

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
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<IList<TDto>> GetListByConditionAsync<TDto>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) where TDto : class, IListViewModel
    {
        var dtos = await _dbSet
                         .AsNoTracking()
                         .Where(expression)
                         .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
        if (dtos.Any() is false)
        {
            throw new NotFoundException();
        }

        return dtos;
    }

    /// <summary>
    /// Filters a sequence of values based on a predicate
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity</typeparam>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <returns>The entity found that mapped to TDto.</returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public IList<TDto> GetListByCondition<TDto>(Expression<Func<TEntity, bool>> expression) where TDto : class, IListViewModel
    {
        var dtos = _dbSet
                   .AsNoTracking()
                   .Where(expression)
                   .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                   .ToList();
        if (dtos.Any() is false)
        {
            throw new NotFoundException();
        }

        return dtos;
    }

    /// <summary>
    /// Asynchronously filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///  A task that represents the asynchronous operation. 
    ///  The task result contains a IList of TEntity that filters a sequence of values based on a predicate that mapped to TEntity.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<IList<TEntity>> GetListByConditionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        var entities = await _dbSet
                             .AsNoTracking()
                             .Where(expression)
                             .ToListAsync(cancellationToken);
        if (entities.Any() is false)
        {
            throw new NotFoundException();
        }

        return entities;
    }

    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    /// <param name="expression">A function to test each element for a condition.</param>
    /// <returns>
    ///  Result contains a IList<TEntity> that filters a sequence of values based on a predicate that mapped to TEntity.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public IList<TEntity> GetListByCondition(Expression<Func<TEntity, bool>> expression)
    {
        var entities = _dbSet
                       .AsNoTracking()
                       .Where(expression)
                       .ToList();
        if (entities.Any() is false)
        {
            throw new NotFoundException();
        }

        return entities;
    }

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains entity found that its type is TDto.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception >AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<TDto> GetByIdAsync<TDto>(int id, CancellationToken cancellationToken) where TDto : class, IViewModel
    {
        var dto = await _dbSet
                        .AsNoTracking()
                        .Where(p => p.Id == id)
                        .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken);
        if (dto is null)
        {
            throw new NotFoundException();
        }

        return dto;
    }

    /// <summary>
    /// Finds an entity with the given primary key values.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <returns>Result contains entity found that its type is TDto.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception >AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public TDto GetById<TDto>(int id) where TDto : class, IViewModel
    {
        var dto = _dbSet
                  .AsNoTracking()
                  .Where(p => p.Id == id)
                  .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefault();
        if (dto is null)
        {
            throw new NotFoundException();
        }

        return dto;
    }

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains entity found that its type is TEntity.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
                           .AsNoTracking()
                           .Where(p => p.Id == id)
                           .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

    /// <summary>
    /// Asynchronously finds an entity with the given primary key values.
    /// </summary>
    /// <param name="id">The values of the primary key for the entity to be found.</param>
    /// <returns>Result contains entity found that its type is TEntity.
    /// </returns>
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    public TEntity GetById(int id)
    {
        var entity = _dbSet
                     .AsNoTracking()
                     .Where(p => p.Id == id)
                     .FirstOrDefault();
        if (entity is null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

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
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception >AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<TDto> GetByConditionAsync<TDto>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken, bool isFirstOrDefault = true) where TDto : class, IViewModel
    {
        TDto dto;
        if (isFirstOrDefault)
        {
            dto = await _dbSet
                        .AsNoTracking()
                        .Where(expression)
                        .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            dto = await _dbSet
                        .AsNoTracking()
                        .Where(expression)
                        .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                        .LastOrDefaultAsync(cancellationToken);
        }

        if (dto is null)
        {
            throw new NotFoundException();
        }

        return dto;
    }

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
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    /// <exception >AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public TDto GetByCondition<TDto>(Expression<Func<TEntity, bool>> expression, bool isFirstOrDefault = true) where TDto : class, IViewModel
    {
        TDto dto;
        if (isFirstOrDefault)
        {
            dto = _dbSet
                  .AsNoTracking()
                  .Where(expression)
                  .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefault();
        }
        else
        {
            dto = _dbSet
                  .AsNoTracking()
                  .Where(expression)
                  .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                  .LastOrDefault();
        }

        if (dto is null)
        {
            throw new NotFoundException();
        }

        return dto;
    }

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
    /// <exception cref="NotFoundException">Occured when does not exists any records with expression.</exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    public async Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken, bool isFirstOrDefault = true)
    {
        TEntity entity;
        if (isFirstOrDefault)
        {
            entity = await _dbSet
                           .AsNoTracking()
                           .Where(expression)
                           .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            entity = await _dbSet
                           .AsNoTracking()
                           .Where(expression)
                           .LastOrDefaultAsync(cancellationToken);
        }

        if (entity is null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

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
    /// <exception cref="NotFoundException">Does not exists eny records with expression </exception>
    /// <exception cref="ArgumentNullException">Occured when expression is null.</exception>
    public TEntity GetByCondition(Expression<Func<TEntity, bool>> expression, bool isFirstOrDefault = true)
    {
        TEntity entity;
        if (isFirstOrDefault)
        {
            entity = _dbSet
                     .AsNoTracking()
                     .Where(expression)
                     .FirstOrDefault();
        }
        else
        {
            entity = _dbSet
                     .AsNoTracking()
                     .Where(expression)
                     .LastOrDefault();
        }


        if (entity is null)
        {
            throw new NotFoundException();
        }

        return entity;
    }

    #endregion Get

    #region Add

    /// <summary>
    /// Asynchronously adds given model that its type is TDto to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModel">The TDto to add.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a integer named Id of entity that added to database.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<int> AddAsync<TDto>(TDto createUpdateViewModel, CancellationToken cancellationToken) where TDto : class, ICreateViewModel
    {
        if (createUpdateViewModel is null)
        {
            throw new ArgumentNullException();
        }

        var entity = _mapper.Map<TDto, TEntity>(createUpdateViewModel);
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    /// <summary>
    /// Adds given model that its type is TDto to database
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModel">The TDto to add.</param>
    /// <returns>
    /// Result contains a integer named Id of entity that added to database.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public int Add<TDto>(TDto createUpdateViewModel) where TDto : class, ICreateViewModel
    {
        if (createUpdateViewModel is null)
        {
            throw new ArgumentNullException();
        }

        var entity = _mapper.Map<TDto, TEntity>(createUpdateViewModel);
        _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Asynchronously adds given entity that its type is TEntity to database.
    /// </summary>
    /// <param name="entity">The entity to add that its type is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// Id of entity that added to database.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when entity is null.</exception>
    public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    /// <summary>
    /// Adds given entity that its type is TEntity to database.
    /// </summary>
    /// <param name="entity">The entity to add that its type is TEntity.</param>
    /// <returns>Id of entity that added to database.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when entity is null.</exception>
    public int Add(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }

        _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return entity.Id;
    }

    /// <summary>
    /// Asynchronously adds given models that their type is TDto to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModels">Entities to add that their types is TDto.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task AddRangeAsync<TDto>(IEnumerable<TDto> createUpdateViewModels, CancellationToken cancellationToken) where TDto : class, ICreateViewModel
    {
        if (createUpdateViewModels is null)
        {
            throw new ArgumentNullException();
        }

        var entities = _mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(createUpdateViewModels);
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds given models that their type is TDto to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="createUpdateViewModels">Entities to add that their types is TDto.</param>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public void AddRange<TDto>(IEnumerable<TDto> createUpdateViewModels) where TDto : class, ICreateViewModel
    {
        if (createUpdateViewModels is null)
        {
            throw new ArgumentNullException();
        }

        var entities = _mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(createUpdateViewModels);
        _dbSet.AddRange(entities);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Asynchronously adds given models that their type is TEntity to database.
    /// </summary> 
    /// <param name="entities">Entities to add that their types is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a IList of integers that contains Id's of entities that have been inserted to database.
    /// </returns>
    /// <exception cref="ArgumentNullException">Occured when entities is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task<List<int>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (entities is null)
        {
            throw new ArgumentNullException();
        }

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entities.Select(p => p.Id).ToList();
    }

    /// <summary>
    /// Adds given models that their type is TEntity to database.
    /// </summary> 
    /// <param name="entities">Entities to add that their types is TEntity.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occured when entities is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public List<int> AddRange(IEnumerable<TEntity> entities)
    {
        if (entities is null)
        {
            throw new ArgumentNullException();
        }

        _dbSet.AddRange(entities);
        _dbContext.SaveChanges();

        return entities.Select(p => p.Id).ToList();
    }

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
    /// <exception cref="NotFoundException">Does not exists eny records with given Id.</exception>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task UpdateAsync<TDto>(int id, TDto createUpdateViewModel, CancellationToken cancellationToken) where TDto : class, IUpdateViewModel
    {
        if (createUpdateViewModel is null)
        {
            throw new ArgumentNullException();
        }

        var entity = await _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException();
        }

        _dbSet.Update(_mapper.Map(createUpdateViewModel, entity));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates given model that its type is TDto with given Id of TEntity to database.
    /// </summary>
    /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="createUpdateViewModel">The entity to update that type is TDto.</param>
    /// <exception cref="NotFoundException">Does not exists eny records with given Id.</exception>
    /// <exception cref="ArgumentNullException">Occured when createUpdateViewModel is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public void Update<TDto>(int id, TDto createUpdateViewModel) where TDto : class, IUpdateViewModel
    {
        if (createUpdateViewModel is null)
        {
            throw new ArgumentNullException();
        }

        var entity = _dbSet.Where(p => p.Id == id).FirstOrDefault();
        if (entity is null)
        {
            throw new NotFoundException();
        }

        _dbSet.Update(_mapper.Map(createUpdateViewModel, entity));
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Asynchronously updates given model that its type is TEntity with given Id of TEntity to database.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="entity">The TEntity to update.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Does not exists eny records with given Id.</exception>
    /// <exception cref="ArgumentNullException">Occured when entity is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }

        var doesExists = await _dbSet.AsNoTracking().AnyAsync(p => p.Id == id, cancellationToken);
        if (doesExists is false)
        {
            throw new NotFoundException();
        }

        entity.Id = id;

        DetachEntityIfExistsInChangeTracker(entity);

        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates given model that its type is TEntity with given Id of TEntity to database.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be updated.</param>
    /// <param name="entity">The TEntity to update.</param>
    /// <exception cref="NotFoundException">Does not exists eny records with given Id.</exception>
    /// <exception cref="ArgumentNullException">Occured when entity is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public void Update(int id, TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }

        var doesExists = _dbSet.AsNoTracking().Any(p => p.Id == id);
        if (doesExists is false)
        {
            throw new NotFoundException();
        }

        entity.Id = id;

        DetachEntityIfExistsInChangeTracker(entity);

        _dbSet.Update(entity);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Asynchronously updates given models that their type is TDto to database.
    /// </summary>
    /// <param name="entities">IEnumerable of TEntity to update.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occured when entities is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (entities is null)
        {
            throw new ArgumentNullException();
        }

        DetachEntityIfExistsInChangeTracker(entities);

        _dbSet.UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates given models that their type is TDto to database.
    /// </summary>
    /// <param name="entities">IEnumerable of TEntity to update.</param>
    /// <exception cref="ArgumentNullException">Occured when entities is null.</exception>
    /// <exception>AutoMapper exceptions about mapping between TDto and TEntity.</exception>
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        if (entities is null)
        {
            throw new ArgumentNullException();
        }

        DetachEntityIfExistsInChangeTracker(entities);

        _dbSet.UpdateRange(entities);
        _dbContext.SaveChanges();
    }

    #endregion Update

    #region Remove

    /// <summary>
    /// Asynchronously removes entity with given Id from database.
    /// </summary>
    /// <param name="id">Id of entity that its type is TEntity.</param>
    /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Occured when not found entity with given id.</exception>
    public async Task RemoveAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
                           .AsNoTracking()
                           .Where(p => p.Id == id)
                           .FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException();
        }

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously removes entity with given Id from database.
    /// </summary>
    /// <param name="id">Id of entity that its type is TEntity.</param>
    /// <exception cref="ArgumentNullException">Occured when not found entity with given id.</exception>
    public void Remove(int id)
    {
        var entity = _dbSet.AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
        if (entity is null)
        {
            throw new NotFoundException();
        }

        _dbSet.Remove(entity);
        _dbContext.SaveChangesAsync();
    }

    #endregion

    #region Utils

    /// <summary>
    /// Detach entity when it already exists in change tracker (usually when Add functions in DataProvider called).
    /// </summary>
    /// <param name="entity">Entity for existence checking.</param>
    public void DetachEntityIfExistsInChangeTracker(TEntity entity)
    {
        var entries = _dbContext.ChangeTracker.Entries<TEntity>().ToList();
        var currentEntry = _dbContext.Entry(entity);
        var foundEntry = entries.Where(p => p.Entity.Id == currentEntry.Entity.Id).FirstOrDefault();

        if (foundEntry is not null)
        {
            foundEntry.State = EntityState.Detached;
        }
    }

    /// <summary>
    /// Detach entities when they already exist in change tracker (usually when Add functions in DataProvider called).
    /// </summary>
    /// <param name="entities">Entities for existence checking.</param>
    public void DetachEntityIfExistsInChangeTracker(IEnumerable<TEntity> entities)
    {
        var entries = _dbContext.ChangeTracker.Entries<TEntity>().ToList();
        foreach (var entity in entities)
        {
            var currentEntry = _dbContext.Entry(entity);
            var foundEntry = entries.Where(p => p.Entity.Id == currentEntry.Entity.Id).FirstOrDefault();

            if (foundEntry is not null)
            {
                foundEntry.State = EntityState.Detached;
            }
        }
    }

    #endregion Utils

    #endregion Methods
}
