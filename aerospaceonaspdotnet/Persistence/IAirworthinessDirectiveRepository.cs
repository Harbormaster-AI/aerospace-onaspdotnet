using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAirworthinessDirectiveRepository
{
    Task<AirworthinessDirective?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AirworthinessDirective>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);
    Task UpdateAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);
    Task DeleteAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);

    Task AddToWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
