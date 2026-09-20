using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IAircraftModelService {

    Task Create(AircraftModel model , CancellationToken cancellationToken);
    Task<bool> Update(AircraftModel model, CancellationToken cancellationToken);
    Task<AircraftModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftModel>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFamily(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFamily(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AircraftModelService : IAircraftModelService
{
    private readonly IAircraftModelRepository _repository;
    private readonly ILogger<AircraftModelService> _logger;

    public AircraftModelService(
        IAircraftModelRepository repository, ILogger<AircraftModelService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AircraftModel model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AircraftModel model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ModelDesignation = model.ModelDesignation;
            existing.AircraftType = model.AircraftType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AircraftModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AircraftModel>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignFamily(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFamily(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromVariants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
