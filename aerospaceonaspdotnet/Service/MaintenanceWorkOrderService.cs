
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IMaintenanceWorkOrderService {

    Task Create(MaintenanceWorkOrder model , CancellationToken cancellationToken);
    Task<bool> Update(MaintenanceWorkOrder model, CancellationToken cancellationToken);
    Task<MaintenanceWorkOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceWorkOrder>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAirworthinessDirective(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAirworthinessDirective(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignServiceBulletin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignServiceBulletin(AssociationRequest request, CancellationToken cancellationToken);


}

public class MaintenanceWorkOrderService : IMaintenanceWorkOrderService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMaintenanceWorkOrderRepository _repository;
    private readonly ILogger<MaintenanceWorkOrderService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MaintenanceWorkOrderService(
        ApplicationTelemetry telemetry,
        IMaintenanceWorkOrderRepository repository,
        ILogger<MaintenanceWorkOrderService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(MaintenanceWorkOrder model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "MaintenanceWorkOrder",
                "CreateMaintenanceWorkOrder",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(MaintenanceWorkOrder model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.WorkOrderNumber = model.WorkOrderNumber;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "MaintenanceWorkOrder",
                "UpdateMaintenanceWorkOrder",
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

    public Task<MaintenanceWorkOrder?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MaintenanceWorkOrder>> GetAll(CancellationToken cancellationToken)
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
                "MaintenanceWorkOrder",
                "UpdateMaintenanceWorkOrder",
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
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignAirworthinessDirective(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AirworthinessDirectiveService>().Get(childRequest, cancellationToken);
            parent.AirworthinessDirective = child;
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

    public async Task<bool> UnassignAirworthinessDirective(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.AirworthinessDirective = null;
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

    public async Task<bool> AssignServiceBulletin(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ServiceBulletinService>().Get(childRequest, cancellationToken);
            parent.ServiceBulletin = child;
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

    public async Task<bool> UnassignServiceBulletin(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MaintenanceWorkOrder found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ServiceBulletin = null;
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




}
