using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftModelRepository : IAircraftModelRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftModelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftModels
            .Include(x => x.Family)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftModels
            .AsNoTracking()
            .Include(x => x.Family)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Add(aircraftModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Update(aircraftModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Remove(aircraftModel);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
