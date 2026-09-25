
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftRepository : IAircraftRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Aircraft?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Aircrafts
            .Include(x => x.Variant)
            .Include(x => x.Operator_)
            .Include(x => x.Registration)
            .Include(x => x.Warranty)
            .Include(x => x.ConnectedAircraft)
            .Include(x => x.CabinLayout)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Aircraft>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Aircrafts
            .AsNoTracking()
            .Include(x => x.Variant)
            .Include(x => x.Operator_)
            .Include(x => x.Registration)
            .Include(x => x.Warranty)
            .Include(x => x.ConnectedAircraft)
            .Include(x => x.CabinLayout)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Aircraft aircraft, CancellationToken cancellationToken)
    {
        _db.Aircrafts.Add(aircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Aircraft aircraft, CancellationToken cancellationToken)
    {
        _db.Aircrafts.Update(aircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Aircraft aircraft, CancellationToken cancellationToken)
    {
        _db.Aircrafts.Remove(aircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToMaintenanceRecordsAsync(
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

    public async Task RemoveFromMaintenanceRecordsAsync(
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
