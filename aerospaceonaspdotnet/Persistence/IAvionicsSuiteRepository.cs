using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAvionicsSuiteRepository
{
    Task<AvionicsSuite?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AvionicsSuite>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);
    Task UpdateAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);
    Task DeleteAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);
}
