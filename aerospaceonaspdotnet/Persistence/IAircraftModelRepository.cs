using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftModelRepository
{
    Task<AircraftModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftModel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftModel aircraftModel, CancellationToken cancellationToken);
}
