using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface ISalesRegionRepository
{
    Task<SalesRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesRegion>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesRegion salesRegion, CancellationToken cancellationToken);
    Task UpdateAsync(SalesRegion salesRegion, CancellationToken cancellationToken);
    Task DeleteAsync(SalesRegion salesRegion, CancellationToken cancellationToken);

    Task AddToOperatorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOperatorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSalesCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSalesCampaignsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
