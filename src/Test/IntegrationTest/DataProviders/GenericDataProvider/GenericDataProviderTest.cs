using AutoMapper;
using Data;
using Data.DataProviders;
using Data.Entities.Tickets;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Service.DomainDto.Ticket;
using Xunit;

namespace IntegrationTest.DataProviders.GenericDataProvider;

public class GenericDataProviderTest
{
    private readonly IDataProvider<TicketType> _dataProvider;

    private readonly ApplicationDbContext _applicationDbContext;

    private readonly DbSet<TicketType> _dbSet;

    private readonly IMapper _mapper;

    public GenericDataProviderTest()
    {
        _dataProvider = new DataProvider<TicketType>(new ApplicationDbContext(DataProviderSetup.GetDbContextOptions()), DataProviderSetup.GetAutoMapper());
        _applicationDbContext = new ApplicationDbContext(DataProviderSetup.GetDbContextOptions());
        _dbSet = _applicationDbContext.Set<TicketType>();
        _mapper = DataProviderSetup.GetAutoMapper();
    }

    #region Methods

    #region Get

    [Fact]
    public async Task GetAllAsync_WithMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetAllAsync_WithMapper_1_"},
                new TicketType { Type = "_GetAllAsync_WithMapper_2_"},
            };

        await _dbSet.AddRangeAsync(ticketTypes);
        await _applicationDbContext.SaveChangesAsync();

        var expected = _mapper.Map<List<TicketTypeListDto>>(await _dbSet.Where(p => p.Type.Contains("_GetAllAsync_WithMapper_")).ToListAsync());

        // Act
        var actual = await _dataProvider.GetAllAsync<TicketTypeListDto>(default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetAll_WithMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetAll_WithMapper_1_"},
                new TicketType { Type = "_GetAll_WithMapper_2_"},
            };

        _dbSet.AddRange(ticketTypes);
        _applicationDbContext.SaveChanges();

        var expected = _mapper.Map<List<TicketTypeListDto>>(_dbSet.Where(p => p.Type.Contains("_GetAll_WithMapper_")).ToList());

        // Act
        var actual = _dataProvider.GetAll<TicketTypeListDto>();

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetAllAsync_WithoutMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetAllAsync_WithoutMapper_1_"},
                new TicketType { Type = "_GetAllAsync_WithoutMapper_2_"},
            };

        await _dbSet.AddRangeAsync(ticketTypes);
        await _applicationDbContext.SaveChangesAsync();

        var expected = await _dbSet.Where(p => p.Type.Contains("_GetAllAsync_WithoutMapper_")).ToListAsync();

        // Act
        var actual = await _dataProvider.GetAllAsync(default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetAll_WithoutMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetAll_WithoutMapper_1_"},
                new TicketType { Type = "_GetAll_WithoutMapper_2_"},
            };

        _dbSet.AddRangeAsync(ticketTypes);
        _applicationDbContext.SaveChanges();

        var expected = _dbSet.Where(p => p.Type.Contains("_GetAll_WithoutMapper_")).ToList();

        // Act
        var actual = _dataProvider.GetAll();

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetListByConditionAsync_WithMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetListByConditionAsync_WithMapper_1_"},
                new TicketType { Type = "_GetListByConditionAsync_WithMapper_2_"},
            };

        await _dbSet.AddRangeAsync(ticketTypes);
        await _applicationDbContext.SaveChangesAsync();

        var expected = _mapper.Map<List<TicketTypeListDto>>(await _dbSet.Where(p => p.Type.Contains("_GetListByConditionAsync_WithMapper_")).ToListAsync());

        // Act
        var actual = await _dataProvider.GetListByConditionAsync<TicketTypeListDto>(p => p.Type.Contains("_GetListByConditionAsync_WithMapper_"), default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetListByCondition_WithMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetListByCondition_WithMapper_1_"},
                new TicketType { Type = "_GetListByCondition_WithMapper_2_"},
            };

        _dbSet.AddRange(ticketTypes);
        _applicationDbContext.SaveChanges();

        var expected = _mapper.Map<List<TicketTypeListDto>>(_dbSet.Where(p => p.Type.Contains("_GetListByCondition_WithMapper_")).ToList());

        // Act
        var actual = _dataProvider.GetListByCondition<TicketTypeListDto>(p => p.Type.Contains("_GetListByCondition_WithMapper_"));

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetListByConditionAsync_WithoutMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetListByConditionAsync_WithoutMapper_1_"},
                new TicketType { Type = "_GetListByConditionAsync_WithoutMapper_2_"},
            };

        await _dbSet.AddRangeAsync(ticketTypes);
        await _applicationDbContext.SaveChangesAsync();

        var expected = await _dbSet.Where(p => p.Type.Contains("_GetListByConditionAsync_WithoutMapper_")).ToListAsync();

        // Act
        var actual = await _dataProvider.GetListByConditionAsync(p => p.Type.Contains("_GetListByConditionAsync_WithoutMapper_"), default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetListByCondition_WithoutMapper()
    {
        // Arrange
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_GetListByCondition_WithoutMapper_1_"},
                new TicketType { Type = "_GetListByCondition_WithoutMapper_2_"},
            };

        _dbSet.AddRange(ticketTypes);
        _applicationDbContext.SaveChanges();

        var expected = _dbSet.Where(p => p.Type.Contains("_GetListByCondition_WithoutMapper_")).ToList();

        // Act
        var actual = _dataProvider.GetListByCondition(p => p.Type.Contains("_GetListByCondition_WithoutMapper_"));

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetByIdAsync_WithMapper()
    {
        // Arrange
        var ticketType = new TicketType
        {
            Type = "_GetByIdAsync_WithMapper_"
        };

        await _dbSet.AddAsync(ticketType);
        await _applicationDbContext.SaveChangesAsync();

        var expected = _mapper.Map<TicketTypeDto>(ticketType);

        // Act
        var actual = await _dataProvider.GetByIdAsync<TicketTypeDto>(ticketType.Id, default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetById_WithMapper()
    {
        // Arrange
        var ticketType = new TicketType
        {
            Type = "_GetById_WithMapper_"
        };

        _dbSet.Add(ticketType);
        _applicationDbContext.SaveChanges();

        var expected = _mapper.Map<TicketTypeDto>(ticketType);

        // Act
        var actual = _dataProvider.GetById<TicketTypeDto>(ticketType.Id);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetByIdAsync_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_GetByIdAsync_WithoutMapper_"
        };

        await _dbSet.AddAsync(expected);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var actual = await _dataProvider.GetByIdAsync(expected.Id, default);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetById_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_GetById_WithoutMapper_"
        };

        _dbSet.AddAsync(expected);
        _applicationDbContext.SaveChanges();

        // Act
        var actual = _dataProvider.GetById(expected.Id);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetByConditionAsync_WithMapper()
    {
        // Arrange
        var ticketType = new TicketType
        {
            Type = "_GetByConditionAsync_WithMapper_"
        };

        await _dbSet.AddAsync(ticketType);
        await _applicationDbContext.SaveChangesAsync();

        var expected = _mapper.Map<TicketTypeDto>(ticketType);

        // Act
        var actual = await _dataProvider.GetByConditionAsync<TicketTypeDto>(p => p.Type == ticketType.Type, default, false);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetByCondition_WithMapper()
    {
        // Arrange
        var ticketType = new TicketType
        {
            Type = "_GetByCondition_WithMapper_"
        };

        _dbSet.AddAsync(ticketType);
        _applicationDbContext.SaveChanges();

        var expected = _mapper.Map<TicketTypeDto>(ticketType);

        // Act
        var actual = _dataProvider.GetByCondition<TicketTypeDto>(p => p.Type == ticketType.Type, false);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task GetByConditionAsync_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_GetByConditionAsync_WithoutMapper_"
        };

        await _dbSet.AddAsync(expected);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var actual = await _dataProvider.GetByConditionAsync(p => p.Type == expected.Type, default, false);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void GetByCondition_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_GetByCondition_WithoutMapper_"
        };

        _dbSet.Add(expected);
        _applicationDbContext.SaveChanges();

        // Act
        var actual = _dataProvider.GetByCondition(p => p.Type == expected.Type, false);

        // Assert
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    #endregion Get

    #region Add

    [Fact]
    public async Task AddAsync_WithMapper()
    {
        // Arrange
        var expected = new TicketTypeCreateUpdateDto
        {
            Type = "_AddAsync_WithMapper_"
        };

        // Act
        await _dataProvider.AddAsync(expected, default);

        // Assert
        var result = await _dbSet.AsNoTracking().Where(p => p.Type == expected.Type).LastOrDefaultAsync();
        var actual = new TicketTypeCreateUpdateDto
        {
            Type = result!.Type
        };

        actual.Should().BeEquivalentTo(expected);
        Assert.True(result.Id > 0);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task AddAsync_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_AddAsync_WithoutMapper_"
        };

        // Act
        await _dataProvider.AddAsync(expected, default);

        // Assert
        var actual = await _dbSet.AsNoTracking().Where(p => p.Type == expected.Type).LastOrDefaultAsync();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void Add_WithMapper()
    {
        // Arrange
        var expected = new TicketTypeCreateUpdateDto
        {
            Type = "_Add_WithMapper_"
        };

        // Act
        _dataProvider.Add(expected);

        // Assert
        var result = _dbSet.AsNoTracking().Where(p => p.Type == expected.Type).LastOrDefault();
        var actual = new TicketTypeCreateUpdateDto
        {
            Type = result!.Type
        };

        actual.Should().BeEquivalentTo(expected);
        Assert.True(result.Id > 0);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void Add_WithoutMapper()
    {
        // Arrange
        var expected = new TicketType
        {
            Type = "_Add_WithoutMapper_"
        };

        // Act
        _dataProvider.Add(expected);

        // Assert
        var actual = _dbSet.AsNoTracking().Where(p => p.Type == expected.Type).LastOrDefault();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task AddRangeAsync_WithMapper()
    {
        // Arrange
        var expected = new List<TicketTypeCreateUpdateDto>
            {
                new TicketTypeCreateUpdateDto { Type = "_AddRangeAsync_WithMapper_1_"},
                new TicketTypeCreateUpdateDto { Type = "_AddRangeAsync_WithMapper_2_"},
            };

        // Act
        await _dataProvider.AddRangeAsync(expected, default);

        // Assert
        var result = await _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRangeAsync_WithMapper_")).ToListAsync();
        var actual = new List<TicketTypeCreateUpdateDto>();

        // Mapping TicketTypeListViewModel to TicketTypeCreateUpdateViewModel
        foreach (var item in result)
        {
            actual.Add(new TicketTypeCreateUpdateDto { Type = item.Type });
        }

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task AddRangeAsync_WithoutMapper()
    {
        // Arrange
        var expected = new List<TicketType>
            {
                new TicketType { Type = "_AddRangeAsync_WithoutMapper_1_"},
                new TicketType { Type = "_AddRangeAsync_WithoutMapper_2_"},
            };

        // Act
        await _dataProvider.AddRangeAsync(expected, default);

        // Assert
        var actual = await _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRangeAsync_WithoutMapper_")).ToListAsync();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void AddRange_WithMapper()
    {
        // Arrange
        var expected = new List<TicketTypeCreateUpdateDto>
            {
                new TicketTypeCreateUpdateDto { Type = "_AddRangeAsync_WithMapper_1_"},
                new TicketTypeCreateUpdateDto { Type = "_AddRangeAsync_WithMapper_2_"},
            };

        // Act
        _dataProvider.AddRange(expected);

        // Assert
        var result = _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRangeAsync_WithMapper_")).ToList();
        var actual = new List<TicketTypeCreateUpdateDto>();

        // Mapping TicketTypeListViewModel to TicketTypeCreateUpdateViewModel
        foreach (var item in result)
        {
            actual.Add(new TicketTypeCreateUpdateDto { Type = item.Type });
        }

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void AddRange_WithoutMapper()
    {
        // Arrange
        var expected = new List<TicketType>
            {
                new TicketType { Type = "_AddRangeAsync_WithoutMapper_1_"},
                new TicketType { Type = "_AddRangeAsync_WithoutMapper_2_"},
            };

        // Act
        _dataProvider.AddRange(expected);

        // Assert
        var actual = _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRangeAsync_WithoutMapper_")).ToList();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    #endregion Add

    #region Update

    [Fact]
    public async Task UpdateAsync_WithMapper()
    {
        // Arrange
        // Add TicketType to database for update that
        var expected = new TicketType
        {
            Type = "_AddAsyncForUpdateAsync_WithMapper_"
        };

        await _dbSet.AddAsync(expected);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var actual = new TicketTypeCreateUpdateDto { Type = "_UpdateAsync_WithMapper_" };
        await _dataProvider.UpdateAsync(expected.Id, actual, default);

        // Assert
        expected = await _dbSet.AsNoTracking().Where(p => p.Id == expected.Id).LastOrDefaultAsync();

        actual.Type.Should().BeEquivalentTo(expected!.Type);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task UpdateAsync_WithoutMapper()
    {
        // Arrange
        // Add TicketType to database for update that
        var expected = new TicketType
        {
            Type = "_AddAsyncForUpdateAsync_WithoutMapper_"
        };

        _dbSet.Add(expected);
        _applicationDbContext.SaveChanges();

        expected.Type = "_UpdateAsync_WithoutMapper_";

        // Act
        await _dataProvider.UpdateAsync(expected.Id, expected, default);

        // Assert
        var actual = await _dbSet.AsNoTracking().Where(p => p.Id == expected.Id).LastOrDefaultAsync();
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void Update_WithMapper()
    {
        // Arrange
        // Add TicketType to database for update that
        var expected = new TicketType
        {
            Type = "_AddForUpdate_WithMapper_"
        };

        _dbSet.Add(expected);
        _applicationDbContext.SaveChanges();

        // Act
        var actual = new TicketTypeCreateUpdateDto { Type = "_Update_WithMapper_" };
        _dataProvider.Update(expected.Id, actual);

        // Assert
        expected = _dbSet.AsNoTracking().Where(p => p.Id == expected.Id).LastOrDefault();

        actual.Type.Should().BeEquivalentTo(expected!.Type);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void Update_WithoutMapper()
    {
        // Arrange
        // Add TicketType to database for update that
        var expected = new TicketType
        {
            Type = "_AddForUpdate_WithoutMapper_"
        };

        var id = _dataProvider.Add(expected);
        expected.Type = "_Update_WithoutMapper_";

        // Act
        _dataProvider.Update(id, expected);

        // Assert
        var actual = _dataProvider.GetByCondition(p => p.Id == id);
        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public async Task UpdateRangeAsync()
    {
        // Arrange
        // Add entities to database for update
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_AddRangeAsync_ForUpdateRangeAsync_1_"},
                new TicketType { Type = "_AddRangeAsync_ForUpdateRangeAsync_2_"},
            };

        await _dbSet.AddRangeAsync(ticketTypes);
        await _applicationDbContext.SaveChangesAsync();

        var result = await _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRangeAsync_ForUpdateRangeAsync_")).ToListAsync();
        var expected = new List<TicketType>();

        foreach (var item in result)
        {
            item.Type = $"_UpdateRangeAsync_{item.Id}_";
            expected.Add(item);
        }

        // Act
        await _dataProvider.UpdateRangeAsync(expected, default);

        // Assert
        var actual = await _dbSet.AsNoTracking().Where(p => p.Type.Contains("_UpdateRangeAsync_")).ToListAsync();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    [Fact]
    public void UpdateRange()
    {
        // Arrange
        // Add entities to database for update
        var ticketTypes = new List<TicketType>
            {
                new TicketType { Type = "_AddRange_ForUpdateRange_1_"},
                new TicketType { Type = "_AddRange_ForUpdateRange_2_"},
            };

        _dbSet.AddRangeAsync(ticketTypes);
        _applicationDbContext.SaveChanges();

        var result = _dbSet.AsNoTracking().Where(p => p.Type.Contains("_AddRange_ForUpdateRange_")).ToList();
        var expected = new List<TicketType>();

        foreach (var item in result)
        {
            item.Type = $"_UpdateRange_{item.Id}_";
            expected.Add(item);
        }

        // Act
        _dataProvider.UpdateRange(expected);

        // Assert
        var actual = _dbSet.AsNoTracking().Where(p => p.Type.Contains("_UpdateRange_")).ToList();

        actual.Should().BeEquivalentTo(expected);

        // Delete all inserted records
        DeleteAllRecords();
    }

    #endregion Update

    #region Remove

    [Fact]
    public async Task RemoveAsync()
    {
        // Arrange
        // Add TicketType to database for remove that
        var expected = new TicketType
        {
            Type = "_AddForRemoveAsync_"
        };

        await _dbSet.AddAsync(expected);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        await _dataProvider.RemoveAsync(expected.Id, default);

        // Assert
        var actual = await _dbSet.AsNoTracking().Where(p => p.Id == expected.Id).FirstOrDefaultAsync();
        actual.Should().BeNull();
    }

    [Fact]
    public void Remove()
    {
        // Arrange
        // Add TicketType to database for remove that
        var expected = new TicketType
        {
            Type = "_AddForRemove_"
        };

        _dbSet.Add(expected);
        _applicationDbContext.SaveChanges();

        // Act
        _dataProvider.Remove(expected.Id);

        // Assert
        var actual = _dbSet.AsNoTracking().Where(p => p.Id == expected.Id).FirstOrDefault();
        actual.Should().BeNull();
    }

    #endregion Remove

    #region Utils

    private void DeleteAllRecords()
    {
        // Delete all inserted records
        var allRecords = _dbSet.ToList();
        _dbSet.RemoveRange(allRecords);
        _applicationDbContext.SaveChanges();
    }

    #endregion Utils

    #endregion Methods
}