using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftOptionRepository : IAircraftOptionRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftOptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftOptions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftOption>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftOptions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Add(aircraftOption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Update(aircraftOption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Remove(aircraftOption);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
