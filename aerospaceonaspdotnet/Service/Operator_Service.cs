
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IOperator_Service {

    Task Create(Operator_ model , CancellationToken cancellationToken);
    Task<bool> Update(Operator_ model, CancellationToken cancellationToken);
    Task<Operator_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operator_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSalesRegion(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSalesRegion(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class Operator_Service : IOperator_Service
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IOperator_Repository _repository;
    private readonly ILogger<Operator_Service> _logger;
    private readonly IServiceResolver _serviceResolver;


    public Operator_Service(
        ApplicationTelemetry telemetry,
        IOperator_Repository repository,
        ILogger<Operator_Service> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Operator_ model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Operator_",
                "CreateOperator_",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Operator_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.IcaoDesignator = model.IcaoDesignator;
            existing.OperatorType = model.OperatorType;

            await _telemetry.Execute(
                "Operator_",
                "UpdateOperator_",
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

    public Task<Operator_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Operator_>> GetAll(CancellationToken cancellationToken)
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
                "Operator_",
                "UpdateOperator_",
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

    public async Task<bool> AssignSalesRegion(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Operator_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<SalesRegionService>().Get(childRequest, cancellationToken);
            parent.SalesRegion = child;
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

    public async Task<bool> UnassignSalesRegion(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Operator_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.SalesRegion = null;
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


    public async Task<bool> AddToAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Operator_",
                "AddToAircraftOrders",
                () => _repository.AddToAircraftOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Operator_",
                "RemoveFromAircraftOrders",
                () => _repository.RemoveFromAircraftOrdersAsync(request, cancellationToken));
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

    public async Task<bool> AddToOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Operator_",
                "AddToOperatedAircraft",
                () => _repository.AddToOperatedAircraftAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Operator_",
                "RemoveFromOperatedAircraft",
                () => _repository.RemoveFromOperatedAircraftAsync(request, cancellationToken));
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
