using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ISupplierService {

    Task Create(Supplier model , CancellationToken cancellationToken);
    Task<bool> Update(Supplier model, CancellationToken cancellationToken);
    Task<Supplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToManufacturers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromManufacturers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAvionicsSuites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAvionicsSuites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToApus(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApus(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLandingGears(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLandingGears(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repository;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(
        ISupplierRepository repository, ILogger<SupplierService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Supplier model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Supplier model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.SupplierType = model.SupplierType;
            existing.ApprovalStatus = model.ApprovalStatus;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Supplier?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Supplier>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToManufacturers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromManufacturers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEngineTypes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAvionicsSuites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAvionicsSuites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToApus(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApus(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLandingGears(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLandingGears(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
