using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ILandingGearService {

    Task Create(LandingGear model , CancellationToken cancellationToken);
    Task<bool> Update(LandingGear model, CancellationToken cancellationToken);
    Task<LandingGear?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LandingGear>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LandingGearService : ILandingGearService
{
    private readonly ILandingGearRepository _repository;
    private readonly ILogger<LandingGearService> _logger;

    public LandingGearService(
        ILandingGearRepository repository, ILogger<LandingGearService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LandingGear model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LandingGear model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.SupplierPartNumber = model.SupplierPartNumber;
            existing.GearType = model.GearType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<LandingGear?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LandingGear>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
