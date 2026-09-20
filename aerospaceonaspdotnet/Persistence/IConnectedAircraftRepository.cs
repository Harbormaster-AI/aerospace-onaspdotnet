using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IConnectedAircraftRepository
{
    Task<ConnectedAircraft?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectedAircraft>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);
    Task UpdateAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);
    Task DeleteAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);
}
