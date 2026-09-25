
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IAircraftFamilyService {

    Task Create(AircraftFamily model , CancellationToken cancellationToken);
    Task<bool> Update(AircraftFamily model, CancellationToken cancellationToken);
    Task<AircraftFamily?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftFamily>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProgram(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AircraftFamilyService : IAircraftFamilyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAircraftFamilyRepository _repository;
    private readonly ILogger<AircraftFamilyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AircraftFamilyService(
        ApplicationTelemetry telemetry,
        IAircraftFamilyRepository repository,
        ILogger<AircraftFamilyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AircraftFamily model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AircraftFamily",
                "CreateAircraftFamily",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AircraftFamily model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.FamilyCode = model.FamilyCode;

            await _telemetry.Execute(
                "AircraftFamily",
                "UpdateAircraftFamily",
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

    public Task<AircraftFamily?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AircraftFamily>> GetAll(CancellationToken cancellationToken)
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
                "AircraftFamily",
                "UpdateAircraftFamily",
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

    public async Task<bool> AssignProgram(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftFamily found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AircraftProgramService>().Get(childRequest, cancellationToken);
            parent.Program = child;
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

    public async Task<bool> UnassignProgram(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftFamily found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Program = null;
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


    public async Task<bool> AddToAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftFamily",
                "AddToAircraftModels",
                () => _repository.AddToAircraftModelsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftFamily",
                "RemoveFromAircraftModels",
                () => _repository.RemoveFromAircraftModelsAsync(request, cancellationToken));
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
