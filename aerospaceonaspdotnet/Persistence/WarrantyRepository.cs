
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class WarrantyRepository : IWarrantyRepository
{
    private readonly ApplicationDbContext _db;

    public WarrantyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Warranty?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Warrantys
            .Include(x => x.Aircraft)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Warranty>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Warrantys
            .AsNoTracking()
            .Include(x => x.Aircraft)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Warranty warranty, CancellationToken cancellationToken)
    {
        _db.Warrantys.Add(warranty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Warranty warranty, CancellationToken cancellationToken)
    {
        _db.Warrantys.Update(warranty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Warranty warranty, CancellationToken cancellationToken)
    {
        _db.Warrantys.Remove(warranty);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
