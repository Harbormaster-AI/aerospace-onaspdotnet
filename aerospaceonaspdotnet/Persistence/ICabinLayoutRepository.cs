using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface ICabinLayoutRepository
{
    Task<CabinLayout?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CabinLayout>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CabinLayout cabinLayout, CancellationToken cancellationToken);
    Task UpdateAsync(CabinLayout cabinLayout, CancellationToken cancellationToken);
    Task DeleteAsync(CabinLayout cabinLayout, CancellationToken cancellationToken);
}
