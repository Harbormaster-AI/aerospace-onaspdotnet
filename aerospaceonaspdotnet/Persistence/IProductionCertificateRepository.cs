using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IProductionCertificateRepository
{
    Task<ProductionCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionCertificate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken);
    Task UpdateAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken);
    Task DeleteAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken);


}
