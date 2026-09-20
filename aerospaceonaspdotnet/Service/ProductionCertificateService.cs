using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

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
    private readonly IProductionCertificateRepository _repository;
    private readonly ILogger<ProductionCertificateService> _logger;

    public ProductionCertificateService(
        IProductionCertificateRepository repository, ILogger<ProductionCertificateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ProductionCertificate model, CancellationToken cancellationToken)
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
