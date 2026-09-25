using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Supplier supplier, CancellationToken cancellationToken);
    Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken);
    Task DeleteAsync(Supplier supplier, CancellationToken cancellationToken);

    Task AddToManufacturersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromManufacturersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToComponentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromComponentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEngineTypesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEngineTypesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAvionicsSuitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAvionicsSuitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToApusAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApusAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLandingGearsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLandingGearsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
