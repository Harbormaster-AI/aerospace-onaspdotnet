using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IAerospaceManufacturerRepository
{
    Task<AerospaceManufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AerospaceManufacturer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);
    Task UpdateAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);
    Task DeleteAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);

    Task AddToProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPlantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSuppliersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProductionCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductionCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
