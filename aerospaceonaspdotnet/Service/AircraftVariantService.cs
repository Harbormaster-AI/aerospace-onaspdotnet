
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IAircraftVariantService {

    Task Create(AircraftVariant model , CancellationToken cancellationToken);
    Task<bool> Update(AircraftVariant model, CancellationToken cancellationToken);
    Task<AircraftVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftVariant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignModel_(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignModel_(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEngineType(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEngineType(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignApu(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignApu(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLandingGear(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLandingGear(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPackages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPackages(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AircraftVariantService : IAircraftVariantService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAircraftVariantRepository _repository;
    private readonly ILogger<AircraftVariantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AircraftVariantService(
        ApplicationTelemetry telemetry,
        IAircraftVariantRepository repository,
        ILogger<AircraftVariantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AircraftVariant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AircraftVariant",
                "CreateAircraftVariant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AircraftVariant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.VariantCode = model.VariantCode;
            existing.RangeNm = model.RangeNm;
            existing.MaxTakeoffWeightKg = model.MaxTakeoffWeightKg;

            await _telemetry.Execute(
                "AircraftVariant",
                "UpdateAircraftVariant",
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

    public Task<AircraftVariant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AircraftVariant>> GetAll(CancellationToken cancellationToken)
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
                "AircraftVariant",
                "UpdateAircraftVariant",
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

    public async Task<bool> AssignModel_(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AircraftModelService>().Get(childRequest, cancellationToken);
            parent.Model_ = child;
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

    public async Task<bool> UnassignModel_(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Model_ = null;
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

    public async Task<bool> AssignEngineType(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EngineTypeService>().Get(childRequest, cancellationToken);
            parent.EngineType = child;
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

    public async Task<bool> UnassignEngineType(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.EngineType = null;
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

    public async Task<bool> AssignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AvionicsSuiteService>().Get(childRequest, cancellationToken);
            parent.AvionicsSuite = child;
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

    public async Task<bool> UnassignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.AvionicsSuite = null;
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

    public async Task<bool> AssignApu(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<APUService>().Get(childRequest, cancellationToken);
            parent.Apu = child;
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

    public async Task<bool> UnassignApu(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Apu = null;
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

    public async Task<bool> AssignLandingGear(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<LandingGearService>().Get(childRequest, cancellationToken);
            parent.LandingGear = child;
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

    public async Task<bool> UnassignLandingGear(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AircraftVariant found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.LandingGear = null;
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


    public async Task<bool> AddToCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "AddToCabinLayouts",
                () => _repository.AddToCabinLayoutsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "RemoveFromCabinLayouts",
                () => _repository.RemoveFromCabinLayoutsAsync(request, cancellationToken));
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

    public async Task<bool> AddToOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "AddToOptions",
                () => _repository.AddToOptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "RemoveFromOptions",
                () => _repository.RemoveFromOptionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "AddToPackages",
                () => _repository.AddToPackagesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AircraftVariant",
                "RemoveFromPackages",
                () => _repository.RemoveFromPackagesAsync(request, cancellationToken));
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
