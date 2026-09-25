using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IConnectedAircraftRepository
{
    Task<ConnectedAircraft?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectedAircraft>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);
    Task UpdateAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);
    Task DeleteAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken);

    Task AddToFlightHealthEventsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFlightHealthEventsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSoftwareLoadsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSoftwareLoadsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
