using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IBuildScheduleRepository
{
    Task<BuildSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BuildSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);

    Task AddToProductionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductionOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
