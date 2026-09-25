using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAvionicsSuiteRepository
{
    Task<AvionicsSuite?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AvionicsSuite>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);
    Task UpdateAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);
    Task DeleteAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken);

    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSoftwareLoadsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSoftwareLoadsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
