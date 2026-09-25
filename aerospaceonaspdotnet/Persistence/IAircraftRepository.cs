using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftRepository
{
    Task<Aircraft?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Aircraft>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Aircraft aircraft, CancellationToken cancellationToken);
    Task UpdateAsync(Aircraft aircraft, CancellationToken cancellationToken);
    Task DeleteAsync(Aircraft aircraft, CancellationToken cancellationToken);

    Task AddToMaintenanceRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenanceRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
