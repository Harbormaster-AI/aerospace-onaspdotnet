using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftFamilyRepository
{
    Task<AircraftFamily?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftFamily>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken);

    Task AddToAircraftModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAircraftModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
