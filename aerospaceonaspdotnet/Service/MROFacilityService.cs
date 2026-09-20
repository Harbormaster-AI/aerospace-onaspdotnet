using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IMROFacilityService {

    Task Create(MROFacility model , CancellationToken cancellationToken);
    Task<bool> Update(MROFacility model, CancellationToken cancellationToken);
    Task<MROFacility?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MROFacility>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MROFacilityService : IMROFacilityService
{
    private readonly IMROFacilityRepository _repository;
    private readonly ILogger<MROFacilityService> _logger;

    public MROFacilityService(
        IMROFacilityRepository repository, ILogger<MROFacilityService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MROFacility model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MROFacility model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ApprovalScope = model.ApprovalScope;
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

    public Task<MROFacility?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MROFacility>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAppointments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
