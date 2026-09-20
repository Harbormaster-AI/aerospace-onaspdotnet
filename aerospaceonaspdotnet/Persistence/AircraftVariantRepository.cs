using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftVariantRepository : IAircraftVariantRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftVariantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftVariants
            .Include(x => x.Model)
            .Include(x => x.EngineType)
            .Include(x => x.AvionicsSuite)
            .Include(x => x.Apu)
            .Include(x => x.LandingGear)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftVariant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftVariants
            .AsNoTracking()
            .Include(x => x.Model)
            .Include(x => x.EngineType)
            .Include(x => x.AvionicsSuite)
            .Include(x => x.Apu)
            .Include(x => x.LandingGear)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Add(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Update(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Remove(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
