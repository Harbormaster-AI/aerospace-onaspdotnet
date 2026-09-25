using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftOptionRepository
{
    Task<AircraftOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftOption>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftOption aircraftOption, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftOption aircraftOption, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftOption aircraftOption, CancellationToken cancellationToken);

    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPackagesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPackagesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
