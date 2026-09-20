using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAirworthinessDirectiveRepository
{
    Task<AirworthinessDirective?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AirworthinessDirective>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);
    Task UpdateAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);
    Task DeleteAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken);
}
