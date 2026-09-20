using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class LandingGearRepository : ILandingGearRepository
{
    private readonly ApplicationDbContext _db;

    public LandingGearRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LandingGear?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LandingGears
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LandingGear>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LandingGears
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Add(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Update(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Remove(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
