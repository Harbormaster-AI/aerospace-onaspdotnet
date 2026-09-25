
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class SoftwareLoadRepository : ISoftwareLoadRepository
{
    private readonly ApplicationDbContext _db;

    public SoftwareLoadRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SoftwareLoad?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SoftwareLoads
            .Include(x => x.ConnectedAircraft)
            .Include(x => x.AvionicsSuite)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SoftwareLoad>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SoftwareLoads
            .AsNoTracking()
            .Include(x => x.ConnectedAircraft)
            .Include(x => x.AvionicsSuite)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken)
    {
        _db.SoftwareLoads.Add(softwareLoad);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken)
    {
        _db.SoftwareLoads.Update(softwareLoad);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SoftwareLoad softwareLoad, CancellationToken cancellationToken)
    {
        _db.SoftwareLoads.Remove(softwareLoad);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
