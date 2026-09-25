using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftPackageRepository
{
    Task<AircraftPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftPackage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken);

    Task AddToOptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
