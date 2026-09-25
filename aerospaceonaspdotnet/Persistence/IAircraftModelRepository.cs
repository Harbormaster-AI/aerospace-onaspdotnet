using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftModelRepository
{
    Task<AircraftModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftModel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);

    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEngineTypesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEngineTypesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
