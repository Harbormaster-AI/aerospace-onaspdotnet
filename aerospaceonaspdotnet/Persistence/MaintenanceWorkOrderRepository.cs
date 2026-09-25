
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class MaintenanceWorkOrderRepository : IMaintenanceWorkOrderRepository
{
    private readonly ApplicationDbContext _db;

    public MaintenanceWorkOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MaintenanceWorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MaintenanceWorkOrders
            .Include(x => x.Aircraft)
            .Include(x => x.AirworthinessDirective)
            .Include(x => x.ServiceBulletin)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenanceWorkOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MaintenanceWorkOrders
            .AsNoTracking()
            .Include(x => x.Aircraft)
            .Include(x => x.AirworthinessDirective)
            .Include(x => x.ServiceBulletin)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceWorkOrders.Add(maintenanceWorkOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceWorkOrders.Update(maintenanceWorkOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken)
    {
        _db.MaintenanceWorkOrders.Remove(maintenanceWorkOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
