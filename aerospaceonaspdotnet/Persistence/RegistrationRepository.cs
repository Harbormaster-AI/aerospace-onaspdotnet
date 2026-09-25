
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly ApplicationDbContext _db;

    public RegistrationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Registrations
            .Include(x => x.Aircraft)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Registration>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Registrations
            .AsNoTracking()
            .Include(x => x.Aircraft)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Registration registration, CancellationToken cancellationToken)
    {
        _db.Registrations.Add(registration);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Registration registration, CancellationToken cancellationToken)
    {
        _db.Registrations.Update(registration);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Registration registration, CancellationToken cancellationToken)
    {
        _db.Registrations.Remove(registration);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
