using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IOperatorRepository
{
    Task<Operator?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operator>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Operator operator, CancellationToken cancellationToken);
    Task UpdateAsync(Operator operator, CancellationToken cancellationToken);
    Task DeleteAsync(Operator operator, CancellationToken cancellationToken);
}
