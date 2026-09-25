using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IFlightHealthEventRepository
{
    Task<FlightHealthEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FlightHealthEvent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken);
    Task UpdateAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken);
    Task DeleteAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken);


}
