using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class PlantRepository : IPlantRepository
{
    private readonly ApplicationDbContext _db;

    public PlantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Plants
            .Include(x => x.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Plant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Plants
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Add(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Update(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Remove(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
