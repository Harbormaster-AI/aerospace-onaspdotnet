using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IOperator_Repository
{
    Task<Operator_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Operator_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Operator_ operator_, CancellationToken cancellationToken);
    Task UpdateAsync(Operator_ operator_, CancellationToken cancellationToken);
    Task DeleteAsync(Operator_ operator_, CancellationToken cancellationToken);

    Task AddToAircraftOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAircraftOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOperatedAircraftAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOperatedAircraftAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
