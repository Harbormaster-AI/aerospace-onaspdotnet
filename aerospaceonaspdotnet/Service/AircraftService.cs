using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IAircraftService {

    Task Create(Aircraft model , CancellationToken cancellationToken);
    Task<bool> Update(Aircraft model, CancellationToken cancellationToken);
    Task<Aircraft?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Aircraft>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVariant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOperator(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOperator(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRegistration(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRegistration(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWarranty(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWarranty(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCabinLayout(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCabinLayout(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToMaintenanceRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMaintenanceRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AircraftService : IAircraftService
{
    private readonly IAircraftRepository _repository;
    private readonly ILogger<AircraftService> _logger;

    public AircraftService(
        IAircraftRepository repository, ILogger<AircraftService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Aircraft model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Aircraft model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Msn = model.Msn;
            existing.DeliveryDate = model.DeliveryDate;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Aircraft?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Aircraft>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOperator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOperator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRegistration(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRegistration(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWarranty(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWarranty(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCabinLayout(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCabinLayout(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToMaintenanceRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMaintenanceRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
