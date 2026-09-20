using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IFlightHealthEventService {

    Task Create(FlightHealthEvent model , CancellationToken cancellationToken);
    Task<bool> Update(FlightHealthEvent model, CancellationToken cancellationToken);
    Task<FlightHealthEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FlightHealthEvent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignConnectedAircraft(AssociationRequest request, CancellationToken cancellationToken);


}

public class FlightHealthEventService : IFlightHealthEventService
{
    private readonly IFlightHealthEventRepository _repository;
    private readonly ILogger<FlightHealthEventService> _logger;

    public FlightHealthEventService(
        IFlightHealthEventRepository repository, ILogger<FlightHealthEventService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(FlightHealthEvent model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(FlightHealthEvent model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EventCode = model.EventCode;
            existing.Severity = model.Severity;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<FlightHealthEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FlightHealthEvent>> GetAll(CancellationToken cancellationToken)
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




}
