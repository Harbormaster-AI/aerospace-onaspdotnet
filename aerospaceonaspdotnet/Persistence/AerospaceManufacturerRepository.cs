using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AerospaceManufacturerRepository : IAerospaceManufacturerRepository
{
    private readonly ApplicationDbContext _db;

    public AerospaceManufacturerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AerospaceManufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AerospaceManufacturers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AerospaceManufacturer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AerospaceManufacturers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Add(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Update(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken)
    {
        _db.AerospaceManufacturers.Remove(aerospaceManufacturer);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
