using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface ISalesRegionRepository
{
    Task<SalesRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesRegion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesRegion salesRegion, CancellationToken cancellationToken);
    Task UpdateAsync(SalesRegion salesRegion, CancellationToken cancellationToken);
    Task DeleteAsync(SalesRegion salesRegion, CancellationToken cancellationToken);
}
