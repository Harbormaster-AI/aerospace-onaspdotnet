using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IAirworthinessDirectiveService {

    Task Create(AirworthinessDirective model , CancellationToken cancellationToken);
    Task<bool> Update(AirworthinessDirective model, CancellationToken cancellationToken);
    Task<AirworthinessDirective?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AirworthinessDirective>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AirworthinessDirectiveService : IAirworthinessDirectiveService
{
    private readonly IAirworthinessDirectiveRepository _repository;
    private readonly ILogger<AirworthinessDirectiveService> _logger;

    public AirworthinessDirectiveService(
        IAirworthinessDirectiveRepository repository, ILogger<AirworthinessDirectiveService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AirworthinessDirective model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AirworthinessDirective model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.DirectiveNumber = model.DirectiveNumber;
            existing.Title = model.Title;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AirworthinessDirective?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AirworthinessDirective>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
