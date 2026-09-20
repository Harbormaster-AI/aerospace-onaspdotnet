using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class OperatorRepository : IOperatorRepository
{
    private readonly ApplicationDbContext _db;

    public OperatorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Operator?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Operators
            .Include(x => x.SalesRegion)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Operator>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Operators
            .AsNoTracking()
            .Include(x => x.SalesRegion)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Operator operator, CancellationToken cancellationToken)
    {
        _db.Operators.Add(operator);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Operator operator, CancellationToken cancellationToken)
    {
        _db.Operators.Update(operator);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Operator operator, CancellationToken cancellationToken)
    {
        _db.Operators.Remove(operator);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
