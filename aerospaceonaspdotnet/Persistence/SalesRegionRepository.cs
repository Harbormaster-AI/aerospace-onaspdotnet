
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class SalesRegionRepository : ISalesRegionRepository
{
    private readonly ApplicationDbContext _db;

    public SalesRegionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalesRegion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalesRegions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalesRegion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalesRegions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalesRegion salesRegion, CancellationToken cancellationToken)
    {
        _db.SalesRegions.Add(salesRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesRegion salesRegion, CancellationToken cancellationToken)
    {
        _db.SalesRegions.Update(salesRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalesRegion salesRegion, CancellationToken cancellationToken)
    {
        _db.SalesRegions.Remove(salesRegion);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToOperatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Operator_s
            .Where(operator_ =>
                request.ChildIds.Contains(operator_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    operator_ =>
                        EF.Property<Guid?>(
                            operator_,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOperatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Operator_s
            .Where(operator_ =>
                request.ChildIds.Contains(operator_.Id) &&
                EF.Property<Guid?>(
                    operator_,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    operator_ =>
                        EF.Property<Guid?>(
                            operator_,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToSalesCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesCampaigns
            .Where(salesCampaign =>
                request.ChildIds.Contains(salesCampaign.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesCampaign =>
                        EF.Property<Guid?>(
                            salesCampaign,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSalesCampaignsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalesCampaigns
            .Where(salesCampaign =>
                request.ChildIds.Contains(salesCampaign.Id) &&
                EF.Property<Guid?>(
                    salesCampaign,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salesCampaign =>
                        EF.Property<Guid?>(
                            salesCampaign,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
