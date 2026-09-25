
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IPurchaseAgreementService {

    Task Create(PurchaseAgreement model , CancellationToken cancellationToken);
    Task<bool> Update(PurchaseAgreement model, CancellationToken cancellationToken);
    Task<PurchaseAgreement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseAgreement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken);


}

public class PurchaseAgreementService : IPurchaseAgreementService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPurchaseAgreementRepository _repository;
    private readonly ILogger<PurchaseAgreementService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PurchaseAgreementService(
        ApplicationTelemetry telemetry,
        IPurchaseAgreementRepository repository,
        ILogger<PurchaseAgreementService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(PurchaseAgreement model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "PurchaseAgreement",
                "CreatePurchaseAgreement",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(PurchaseAgreement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AgreementNumber = model.AgreementNumber;
            existing.EffectiveDate = model.EffectiveDate;

            await _telemetry.Execute(
                "PurchaseAgreement",
                "UpdatePurchaseAgreement",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<PurchaseAgreement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PurchaseAgreement>> GetAll(CancellationToken cancellationToken)
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
            await _telemetry.Execute(
                "PurchaseAgreement",
                "UpdatePurchaseAgreement",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PurchaseAgreement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AircraftOrderService>().Get(childRequest, cancellationToken);
            parent.AircraftOrder = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PurchaseAgreement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.AircraftOrder = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
