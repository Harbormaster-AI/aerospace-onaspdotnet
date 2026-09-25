
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class BuildScheduleRepository : IBuildScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public BuildScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BuildSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BuildSchedules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BuildSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BuildSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken)
    {
        _db.BuildSchedules.Add(buildSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken)
    {
        _db.BuildSchedules.Update(buildSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken)
    {
        _db.BuildSchedules.Remove(buildSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProductionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionOrders
            .Where(productionOrder =>
                request.ChildIds.Contains(productionOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionOrder =>
                        EF.Property<Guid?>(
                            productionOrder,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductionOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionOrders
            .Where(productionOrder =>
                request.ChildIds.Contains(productionOrder.Id) &&
                EF.Property<Guid?>(
                    productionOrder,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionOrder =>
                        EF.Property<Guid?>(
                            productionOrder,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
