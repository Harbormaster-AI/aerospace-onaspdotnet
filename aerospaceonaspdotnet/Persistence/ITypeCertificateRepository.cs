using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface ITypeCertificateRepository
{
    Task<TypeCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TypeCertificate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken);
    Task UpdateAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken);
    Task DeleteAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken);


}
