using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AvionicsSuiteRepository : IAvionicsSuiteRepository
{
    private readonly ApplicationDbContext _db;

    public AvionicsSuiteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AvionicsSuite?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AvionicsSuites
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AvionicsSuite>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AvionicsSuites
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Add(avionicsSuite);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Update(avionicsSuite);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Remove(avionicsSuite);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
