using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IRegistrationRepository
{
    Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Registration>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Registration registration, CancellationToken cancellationToken);
    Task UpdateAsync(Registration registration, CancellationToken cancellationToken);
    Task DeleteAsync(Registration registration, CancellationToken cancellationToken);


}
