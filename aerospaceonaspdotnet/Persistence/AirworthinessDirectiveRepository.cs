
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AirworthinessDirectiveRepository : IAirworthinessDirectiveRepository
{
    private readonly ApplicationDbContext _db;

    public AirworthinessDirectiveRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AirworthinessDirective?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AirworthinessDirectives
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AirworthinessDirective>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AirworthinessDirectives
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken)
    {
        _db.AirworthinessDirectives.Add(airworthinessDirective);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken)
    {
        _db.AirworthinessDirectives.Update(airworthinessDirective);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AirworthinessDirective airworthinessDirective, CancellationToken cancellationToken)
    {
        _db.AirworthinessDirectives.Remove(airworthinessDirective);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceWorkOrders
            .Where(maintenanceWorkOrder =>
                request.ChildIds.Contains(maintenanceWorkOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceWorkOrder =>
                        EF.Property<Guid?>(
                            maintenanceWorkOrder,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceWorkOrders
            .Where(maintenanceWorkOrder =>
                request.ChildIds.Contains(maintenanceWorkOrder.Id) &&
                EF.Property<Guid?>(
                    maintenanceWorkOrder,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceWorkOrder =>
                        EF.Property<Guid?>(
                            maintenanceWorkOrder,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
