using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IEngineTypeService {

    Task Create(EngineType model , CancellationToken cancellationToken);
    Task<bool> Update(EngineType model, CancellationToken cancellationToken);
    Task<EngineType?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EngineType>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCompatibleModels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompatibleModels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class EngineTypeService : IEngineTypeService
{
    private readonly IEngineTypeRepository _repository;
    private readonly ILogger<EngineTypeService> _logger;

    public EngineTypeService(
        IEngineTypeRepository repository, ILogger<EngineTypeService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(EngineType model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(EngineType model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EngineModelCode = model.EngineModelCode;
            existing.MaxThrustKn = model.MaxThrustKn;
            existing.Category = model.Category;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<EngineType?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EngineType>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToCompatibleModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompatibleModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
