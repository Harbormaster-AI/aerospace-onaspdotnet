
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class SalesCampaignRepository : ISalesCampaignRepository
{
    private readonly ApplicationDbContext _db;

    public SalesCampaignRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalesCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalesCampaigns
            .Include(x => x.Region)
            .Include(x => x.Operator_)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesCampaign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalesCampaigns
            .AsNoTracking()
            .Include(x => x.Region)
            .Include(x => x.Operator_)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken)
    {
        _db.SalesCampaigns.Add(salesCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken)
    {
        _db.SalesCampaigns.Update(salesCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesCampaign salesCampaign, CancellationToken cancellationToken)
    {
        _db.SalesCampaigns.Remove(salesCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToQuotesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Quotes
            .Where(quote =>
                request.ChildIds.Contains(quote.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    quote =>
                        EF.Property<Guid?>(
                            quote,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromQuotesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Quotes
            .Where(quote =>
                request.ChildIds.Contains(quote.Id) &&
                EF.Property<Guid?>(
                    quote,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    quote =>
                        EF.Property<Guid?>(
                            quote,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
