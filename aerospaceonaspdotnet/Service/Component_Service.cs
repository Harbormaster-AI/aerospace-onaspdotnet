using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IComponent_Service {

    Task Create(Component_ model , CancellationToken cancellationToken);
    Task<bool> Update(Component_ model, CancellationToken cancellationToken);
    Task<Component_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Component_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSupplier(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSupplier(AssociationRequest request, CancellationToken cancellationToken);


}

public class Component_Service : IComponent_Service
{
    private readonly IComponent_Repository _repository;
    private readonly ILogger<Component_Service> _logger;

    public Component_Service(
        IComponent_Repository repository, ILogger<Component_Service> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Component_ model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Component_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PartNumber = model.PartNumber;
            existing.Name = model.Name;
            existing.ComponentCategory = model.ComponentCategory;
            existing.SerializationMethod = model.SerializationMethod;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Component_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Component_>> GetAll(CancellationToken cancellationToken)
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




}
