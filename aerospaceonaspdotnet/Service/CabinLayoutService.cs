using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ICabinLayoutService {

    Task Create(CabinLayout model , CancellationToken cancellationToken);
    Task<bool> Update(CabinLayout model, CancellationToken cancellationToken);
    Task<CabinLayout?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CabinLayout>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOptions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CabinLayoutService : ICabinLayoutService
{
    private readonly ICabinLayoutRepository _repository;
    private readonly ILogger<CabinLayoutService> _logger;

    public CabinLayoutService(
        ICabinLayoutRepository repository, ILogger<CabinLayoutService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CabinLayout model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CabinLayout model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.LayoutCode = model.LayoutCode;
            existing.TotalSeats = model.TotalSeats;
            existing.ClassLayout = model.ClassLayout;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CabinLayout?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CabinLayout>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
