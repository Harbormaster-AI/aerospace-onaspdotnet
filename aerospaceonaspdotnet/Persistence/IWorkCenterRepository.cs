using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IWorkCenterRepository
{
    Task<WorkCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkCenter workCenter, CancellationToken cancellationToken);
    Task UpdateAsync(WorkCenter workCenter, CancellationToken cancellationToken);
    Task DeleteAsync(WorkCenter workCenter, CancellationToken cancellationToken);


}
