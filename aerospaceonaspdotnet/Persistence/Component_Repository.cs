
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class Component_Repository : IComponent_Repository
{
    private readonly ApplicationDbContext _db;

    public Component_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Component_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Component_s
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Component_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Component_s
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Component_ component_, CancellationToken cancellationToken)
    {
        _db.Component_s.Add(component_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Component_ component_, CancellationToken cancellationToken)
    {
        _db.Component_s.Update(component_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Component_ component_, CancellationToken cancellationToken)
    {
        _db.Component_s.Remove(component_);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
