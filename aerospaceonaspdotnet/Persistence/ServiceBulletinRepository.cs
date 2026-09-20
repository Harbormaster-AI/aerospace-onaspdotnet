using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class ServiceBulletinRepository : IServiceBulletinRepository
{
    private readonly ApplicationDbContext _db;

    public ServiceBulletinRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceBulletin?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ServiceBulletins
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ServiceBulletin>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ServiceBulletins
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken)
    {
        _db.ServiceBulletins.Add(serviceBulletin);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken)
    {
        _db.ServiceBulletins.Update(serviceBulletin);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ServiceBulletin serviceBulletin, CancellationToken cancellationToken)
    {
        _db.ServiceBulletins.Remove(serviceBulletin);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
