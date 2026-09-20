using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IMaintenanceAppointmentService {

    Task Create(MaintenanceAppointment model , CancellationToken cancellationToken);
    Task<bool> Update(MaintenanceAppointment model, CancellationToken cancellationToken);
    Task<MaintenanceAppointment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceAppointment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignMroFacility(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMroFacility(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class MaintenanceAppointmentService : IMaintenanceAppointmentService
{
    private readonly IMaintenanceAppointmentRepository _repository;
    private readonly ILogger<MaintenanceAppointmentService> _logger;

    public MaintenanceAppointmentService(
        IMaintenanceAppointmentRepository repository, ILogger<MaintenanceAppointmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(MaintenanceAppointment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(MaintenanceAppointment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AppointmentDate = model.AppointmentDate;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<MaintenanceAppointment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MaintenanceAppointment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAircraft(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignMroFacility(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMroFacility(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
