using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class ConnectedAircraftRepository : IConnectedAircraftRepository
{
    private readonly ApplicationDbContext _db;

    public ConnectedAircraftRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ConnectedAircraft?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ConnectedAircrafts
            .Include(x => x.Aircraft)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ConnectedAircraft>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ConnectedAircrafts
            .AsNoTracking()
            .Include(x => x.Aircraft)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken)
    {
        _db.ConnectedAircrafts.Add(connectedAircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken)
    {
        _db.ConnectedAircrafts.Update(connectedAircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ConnectedAircraft connectedAircraft, CancellationToken cancellationToken)
    {
        _db.ConnectedAircrafts.Remove(connectedAircraft);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
