
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IBuildScheduleService {

    Task Create(BuildSchedule model , CancellationToken cancellationToken);
    Task<bool> Update(BuildSchedule model, CancellationToken cancellationToken);
    Task<BuildSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BuildSchedule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToProductionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BuildScheduleService : IBuildScheduleService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IBuildScheduleRepository _repository;
    private readonly ILogger<BuildScheduleService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public BuildScheduleService(
        ApplicationTelemetry telemetry,
        IBuildScheduleRepository repository,
        ILogger<BuildScheduleService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(BuildSchedule model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "BuildSchedule",
                "CreateBuildSchedule",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(BuildSchedule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ScheduleNumber = model.ScheduleNumber;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "BuildSchedule",
                "UpdateBuildSchedule",
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

    public Task<BuildSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BuildSchedule>> GetAll(CancellationToken cancellationToken)
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
                "BuildSchedule",
                "UpdateBuildSchedule",
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


    public async Task<bool> AddToProductionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BuildSchedule",
                "AddToProductionOrders",
                () => _repository.AddToProductionOrdersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProductionOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "BuildSchedule",
                "RemoveFromProductionOrders",
                () => _repository.RemoveFromProductionOrdersAsync(request, cancellationToken));
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
