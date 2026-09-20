using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAPURepository
{
    Task<APU?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<APU>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(APU aPU, CancellationToken cancellationToken);
    Task UpdateAsync(APU aPU, CancellationToken cancellationToken);
    Task DeleteAsync(APU aPU, CancellationToken cancellationToken);
}
