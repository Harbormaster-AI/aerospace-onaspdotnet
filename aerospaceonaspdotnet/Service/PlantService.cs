using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IPlantService {

    Task Create(Plant model , CancellationToken cancellationToken);
    Task<bool> Update(Plant model, CancellationToken cancellationToken);
    Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PlantService : IPlantService
{
    private readonly IPlantRepository _repository;
    private readonly ILogger<PlantService> _logger;

    public PlantService(
        IPlantRepository repository, ILogger<PlantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Plant model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Plant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.PlantCode = model.PlantCode;
            existing.Address = model.Address;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Plant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Plant>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProductionLines(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWarehouses(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
