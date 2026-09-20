using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftFamilyRepository : IAircraftFamilyRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftFamilyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftFamily?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftFamilys
            .Include(x => x.Program)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftFamily>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftFamilys
            .AsNoTracking()
            .Include(x => x.Program)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Add(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Update(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Remove(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
