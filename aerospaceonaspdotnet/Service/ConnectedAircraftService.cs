
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IConnectedAircraftRepository _repository;
    private readonly ILogger<ConnectedAircraftService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ConnectedAircraftService(
        ApplicationTelemetry telemetry,
        IConnectedAircraftRepository repository,
        ILogger<ConnectedAircraftService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ConnectedAircraft model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ConnectedAircraft",
                "CreateConnectedAircraft",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "ConnectedAircraft",
                "UpdateConnectedAircraft",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "ConnectedAircraft",
                "UpdateConnectedAircraft",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ConnectedAircraft found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AircraftService>().Get(childRequest, cancellationToken);
            parent.Aircraft = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ConnectedAircraft found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Aircraft = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ConnectedAircraft",
                "AddToFlightHealthEvents",
                () => _repository.AddToFlightHealthEventsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromFlightHealthEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ConnectedAircraft",
                "RemoveFromFlightHealthEvents",
                () => _repository.RemoveFromFlightHealthEventsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ConnectedAircraft",
                "AddToSoftwareLoads",
                () => _repository.AddToSoftwareLoadsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromSoftwareLoads(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ConnectedAircraft",
                "RemoveFromSoftwareLoads",
                () => _repository.RemoveFromSoftwareLoadsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
