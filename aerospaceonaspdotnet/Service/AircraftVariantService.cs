using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

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
    Task<bool> AssignModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignModel(AssociationRequest request, CancellationToken cancellationToken);
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
    private readonly IAircraftVariantRepository _repository;
    private readonly ILogger<AircraftVariantService> _logger;

    public AircraftVariantService(
        IAircraftVariantRepository repository, ILogger<AircraftVariantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AircraftVariant model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEngineType(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEngineType(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignApu(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignApu(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLandingGear(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLandingGear(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCabinLayouts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
