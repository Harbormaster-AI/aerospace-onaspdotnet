
using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Telemetry;

namespace aerospaceonaspdotnet.Service;

public interface IProductionCertificateService {

    Task Create(ProductionCertificate model , CancellationToken cancellationToken);
    Task<bool> Update(ProductionCertificate model, CancellationToken cancellationToken);
    Task<ProductionCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionCertificate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken);


}

public class ProductionCertificateService : IProductionCertificateService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IProductionCertificateRepository _repository;
    private readonly ILogger<ProductionCertificateService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ProductionCertificateService(
        ApplicationTelemetry telemetry,
        IProductionCertificateRepository repository,
        ILogger<ProductionCertificateService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ProductionCertificate model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ProductionCertificate",
                "CreateProductionCertificate",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ProductionCertificate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CertificateNumber = model.CertificateNumber;
            existing.Authority = model.Authority;

            await _telemetry.Execute(
                "ProductionCertificate",
                "UpdateProductionCertificate",
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

    public Task<ProductionCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ProductionCertificate>> GetAll(CancellationToken cancellationToken)
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
                "ProductionCertificate",
                "UpdateProductionCertificate",
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

    public async Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductionCertificate found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AerospaceManufacturerService>().Get(childRequest, cancellationToken);
            parent.Manufacturer = child;
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

    public async Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ProductionCertificate found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Manufacturer = null;
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
