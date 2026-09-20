using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftVariantRepository
{
    Task<AircraftVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftVariant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken);
}
