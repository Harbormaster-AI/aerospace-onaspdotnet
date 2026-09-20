using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ISoftwareLoadService {

    Task Create(SoftwareLoad model , CancellationToken cancellationToken);
    Task<bool> Update(SoftwareLoad model, CancellationToken cancellationToken);
    Task<SoftwareLoad?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareLoad>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken);


}

public class SoftwareLoadService : ISoftwareLoadService
{
    private readonly ISoftwareLoadRepository _repository;
    private readonly ILogger<SoftwareLoadService> _logger;

    public SoftwareLoadService(
        ISoftwareLoadRepository repository, ILogger<SoftwareLoadService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SoftwareLoad model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(SoftwareLoad model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Version = model.Version;
            existing.LoadType = model.LoadType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<SoftwareLoad?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SoftwareLoad>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAvionicsSuite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
