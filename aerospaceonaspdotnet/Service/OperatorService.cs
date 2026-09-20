using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IOperatorService {

    Task Create(Operator model , CancellationToken cancellationToken);
    Task<bool> Update(Operator model, CancellationToken cancellationToken);
    Task<Operator?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operator>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSalesRegion(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSalesRegion(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OperatorService : IOperatorService
{
    private readonly IOperatorRepository _repository;
    private readonly ILogger<OperatorService> _logger;

    public OperatorService(
        IOperatorRepository repository, ILogger<OperatorService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Operator model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Operator model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.IcaoDesignator = model.IcaoDesignator;
            existing.OperatorType = model.OperatorType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Operator?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Operator>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSalesRegion(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSalesRegion(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAircraftOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOperatedAircraft(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
