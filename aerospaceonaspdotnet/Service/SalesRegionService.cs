
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface ISalesRegionService {

    Task Create(SalesRegion model , CancellationToken cancellationToken);
    Task<bool> Update(SalesRegion model, CancellationToken cancellationToken);
    Task<SalesRegion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesRegion>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToOperators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOperators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSalesCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSalesCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SalesRegionService : ISalesRegionService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISalesRegionRepository _repository;
    private readonly ILogger<SalesRegionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SalesRegionService(
        ApplicationTelemetry telemetry,
        ISalesRegionRepository repository,
        ILogger<SalesRegionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(SalesRegion model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "SalesRegion",
                "CreateSalesRegion",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(SalesRegion model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.RegionCode = model.RegionCode;

            await _telemetry.Execute(
                "SalesRegion",
                "UpdateSalesRegion",
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

    public Task<SalesRegion?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SalesRegion>> GetAll(CancellationToken cancellationToken)
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
                "SalesRegion",
                "UpdateSalesRegion",
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


    public async Task<bool> AddToOperators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SalesRegion",
                "AddToOperators",
                () => _repository.AddToOperatorsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOperators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SalesRegion",
                "RemoveFromOperators",
                () => _repository.RemoveFromOperatorsAsync(request, cancellationToken));
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

    public async Task<bool> AddToSalesCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SalesRegion",
                "AddToSalesCampaigns",
                () => _repository.AddToSalesCampaignsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSalesCampaigns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SalesRegion",
                "RemoveFromSalesCampaigns",
                () => _repository.RemoveFromSalesCampaignsAsync(request, cancellationToken));
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
