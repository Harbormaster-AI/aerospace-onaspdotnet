using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

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
    private readonly IPurchaseAgreementRepository _repository;
    private readonly ILogger<PurchaseAgreementService> _logger;

    public PurchaseAgreementService(
        IPurchaseAgreementRepository repository, ILogger<PurchaseAgreementService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PurchaseAgreement model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAircraftOrder(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
