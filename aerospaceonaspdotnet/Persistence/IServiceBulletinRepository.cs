using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IServiceBulletinRepository
{
    Task<ServiceBulletin?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ServiceBulletin>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);
    Task UpdateAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);
    Task DeleteAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);
}
