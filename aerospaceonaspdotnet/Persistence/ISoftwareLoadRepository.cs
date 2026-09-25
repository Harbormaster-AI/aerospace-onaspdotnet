using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface ISoftwareLoadRepository
{
    Task<SoftwareLoad?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareLoad>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken);
    Task UpdateAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken);
    Task DeleteAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken);


}
