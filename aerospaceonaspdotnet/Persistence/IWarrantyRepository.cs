using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IWarrantyRepository
{
    Task<Warranty?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Warranty>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Warranty warranty, CancellationToken cancellationToken);
    Task UpdateAsync(Warranty warranty, CancellationToken cancellationToken);
    Task DeleteAsync(Warranty warranty, CancellationToken cancellationToken);


}
