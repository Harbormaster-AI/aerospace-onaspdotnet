using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftPackageRepository : IAircraftPackageRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftPackageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftPackages
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftPackage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftPackages
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Add(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Update(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Remove(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
