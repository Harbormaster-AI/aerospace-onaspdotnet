using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

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
    private readonly IAircraftFamilyRepository _repository;
    private readonly ILogger<AircraftFamilyService> _logger;

    public AircraftFamilyService(
        IAircraftFamilyRepository repository, ILogger<AircraftFamilyService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AircraftFamily model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAircraftModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
