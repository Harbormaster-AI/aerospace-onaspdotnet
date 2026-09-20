using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ISalesCampaignService {

    Task Create(SalesCampaign model , CancellationToken cancellationToken);
    Task<bool> Update(SalesCampaign model, CancellationToken cancellationToken);
    Task<SalesCampaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesCampaign>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRegion(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRegion(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOperator(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOperator(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SalesCampaignService : ISalesCampaignService
{
    private readonly ISalesCampaignRepository _repository;
    private readonly ILogger<SalesCampaignService> _logger;

    public SalesCampaignService(
        ISalesCampaignRepository repository, ILogger<SalesCampaignService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SalesCampaign model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(SalesCampaign model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CampaignCode = model.CampaignCode;
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

    public Task<SalesCampaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SalesCampaign>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRegion(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRegion(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOperator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOperator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromQuotes(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
