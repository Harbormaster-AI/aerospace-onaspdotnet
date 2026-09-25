using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IServiceBulletinRepository
{
    Task<ServiceBulletin?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ServiceBulletin>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);
    Task UpdateAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);
    Task DeleteAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken);

    Task AddToWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
