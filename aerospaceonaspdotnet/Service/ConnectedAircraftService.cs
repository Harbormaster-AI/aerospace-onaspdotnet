using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IConnectedAircraftService {

    Task Create(ConnectedAircraft model , CancellationToken cancellationToken);
    Task<bool> Update(ConnectedAircraft model, CancellationToken cancellationToken);
    Task<ConnectedAircraft?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectedAircraft>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ConnectedAircraftService : IConnectedAircraftService
{
    private readonly IConnectedAircraftRepository _repository;
    private readonly ILogger<ConnectedAircraftService> _logger;

    public ConnectedAircraftService(
        IConnectedAircraftRepository repository, ILogger<ConnectedAircraftService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ConnectedAircraft model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(ConnectedAircraft model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CommunicationsProvider = model.CommunicationsProvider;
            existing.ConnectivityStatus = model.ConnectivityStatus;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ConnectedAircraft?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ConnectedAircraft>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
