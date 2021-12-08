using Data.Entities.Tickets;
using Service.Domain.Tickets.Models;

namespace Service.Domain.Tickets.Services;

public class TicketTypeService
{
    #region Fields

    private readonly IDataProvider<TicketType> _TicketTypeDataProvider;

    #endregion

    #region Ctor

    public TicketTypeService(IDataProvider<TicketType> TicketTypeDataProvider)
    {
        _TicketTypeDataProvider = TicketTypeDataProvider;
    }

    #endregion

    #region Methods

    public async Task<IList<TicketTypeListViewModel>> GetAllTicketTypesAsync(CancellationToken cancellationToken)
    {
        return await _TicketTypeDataProvider.GetAllAsync<TicketTypeListViewModel>(cancellationToken);
    }

    public async Task<TicketTypeViewModel> GetTicketTypesByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _TicketTypeDataProvider.GetByIdAsync<TicketTypeViewModel>(id, cancellationToken);
    }

    public async Task CreateAsync(TicketTypeCreateUpdateViewModel TicketTypeCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _TicketTypeDataProvider.AddAsync(TicketTypeCreateOrUpdateViewModel, cancellationToken);
    }

    public async Task UpdateAsync(int id, TicketTypeCreateUpdateViewModel TicketTypeCreateOrUpdateViewModel, CancellationToken cancellationToken)
    {
        await _TicketTypeDataProvider.UpdateAsync(id, TicketTypeCreateOrUpdateViewModel, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _TicketTypeDataProvider.RemoveAsync(id, cancellationToken);
    }

    #endregion
}
