
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class FlightHealthEventRepository : IFlightHealthEventRepository
{
    private readonly ApplicationDbContext _db;

    public FlightHealthEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FlightHealthEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FlightHealthEvents
            .Include(x => x.ConnectedAircraft)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FlightHealthEvent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FlightHealthEvents
            .AsNoTracking()
            .Include(x => x.ConnectedAircraft)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken)
    {
        _db.FlightHealthEvents.Add(flightHealthEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken)
    {
        _db.FlightHealthEvents.Update(flightHealthEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FlightHealthEvent flightHealthEvent, CancellationToken cancellationToken)
    {
        _db.FlightHealthEvents.Remove(flightHealthEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
