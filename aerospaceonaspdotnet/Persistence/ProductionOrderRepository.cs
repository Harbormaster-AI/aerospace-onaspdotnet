
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class ProductionOrderRepository : IProductionOrderRepository
{
    private readonly ApplicationDbContext _db;

    public ProductionOrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductionOrders
            .Include(x => x.Variant)
            .Include(x => x.Plant)
            .Include(x => x.AircraftOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductionOrders
            .AsNoTracking()
            .Include(x => x.Variant)
            .Include(x => x.Plant)
            .Include(x => x.AircraftOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductionOrder productionOrder, CancellationToken cancellationToken)
    {
        _db.ProductionOrders.Add(productionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductionOrder productionOrder, CancellationToken cancellationToken)
    {
        _db.ProductionOrders.Update(productionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductionOrder productionOrder, CancellationToken cancellationToken)
    {
        _db.ProductionOrders.Remove(productionOrder);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
