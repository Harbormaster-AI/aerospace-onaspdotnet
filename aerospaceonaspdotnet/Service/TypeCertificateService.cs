using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface ITypeCertificateService {

    Task Create(TypeCertificate model , CancellationToken cancellationToken);
    Task<bool> Update(TypeCertificate model, CancellationToken cancellationToken);
    Task<TypeCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TypeCertificate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProgram(AssociationRequest request, CancellationToken cancellationToken);


}

public class TypeCertificateService : ITypeCertificateService
{
    private readonly ITypeCertificateRepository _repository;
    private readonly ILogger<TypeCertificateService> _logger;

    public TypeCertificateService(
        ITypeCertificateRepository repository, ILogger<TypeCertificateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TypeCertificate model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(TypeCertificate model, CancellationToken cancellationToken)
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

    public Task<TypeCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TypeCertificate>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
