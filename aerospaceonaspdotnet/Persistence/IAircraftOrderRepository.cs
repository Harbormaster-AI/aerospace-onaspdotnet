using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftOrderRepository
{
    Task<AircraftOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken);


}
