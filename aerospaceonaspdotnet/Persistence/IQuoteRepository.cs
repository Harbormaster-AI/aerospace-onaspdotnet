using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IQuoteRepository
{
    Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Quote>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Quote quote, CancellationToken cancellationToken);
    Task UpdateAsync(Quote quote, CancellationToken cancellationToken);
    Task DeleteAsync(Quote quote, CancellationToken cancellationToken);


}
