using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftProgramRepository : IAircraftProgramRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftProgramRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftPrograms
            .Include(x => x.Manufacturer)
            .Include(x => x.TypeCertificate)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftProgram>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftPrograms
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .Include(x => x.TypeCertificate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken)
    {
        _db.AircraftPrograms.Add(aircraftProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken)
    {
        _db.AircraftPrograms.Update(aircraftProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken)
    {
        _db.AircraftPrograms.Remove(aircraftProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
