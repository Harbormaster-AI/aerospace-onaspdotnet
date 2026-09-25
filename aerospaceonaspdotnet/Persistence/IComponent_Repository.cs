using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IComponent_Repository
{
    Task<Component_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Component_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Component_ component_, CancellationToken cancellationToken);
    Task UpdateAsync(Component_ component_, CancellationToken cancellationToken);
    Task DeleteAsync(Component_ component_, CancellationToken cancellationToken);


}
