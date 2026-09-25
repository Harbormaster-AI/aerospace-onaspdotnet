
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftOrderRepository : IAircraftOrderRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftOrders
            .Include(x => x.Operator_)
            .Include(x => x.Variant)
            .Include(x => x.Quote)
            .Include(x => x.PurchaseAgreement)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftOrders
            .AsNoTracking()
            .Include(x => x.Operator_)
            .Include(x => x.Variant)
            .Include(x => x.Quote)
            .Include(x => x.PurchaseAgreement)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken)
    {
        _db.AircraftOrders.Add(aircraftOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken)
    {
        _db.AircraftOrders.Update(aircraftOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftOrder aircraftOrder, CancellationToken cancellationToken)
    {
        _db.AircraftOrders.Remove(aircraftOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
