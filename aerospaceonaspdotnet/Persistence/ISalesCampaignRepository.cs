using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface ISalesCampaignRepository
{
    Task<SalesCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalesCampaign>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken);
    Task UpdateAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken);
    Task DeleteAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken);
}
