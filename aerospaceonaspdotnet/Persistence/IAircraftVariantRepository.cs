using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftVariantRepository
{
    Task<AircraftVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftVariant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);

    Task AddToCabinLayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCabinLayoutsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPackagesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPackagesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
