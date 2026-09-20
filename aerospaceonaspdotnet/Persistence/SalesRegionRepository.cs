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
}
