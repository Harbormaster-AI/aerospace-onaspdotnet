
using aerospaceonaspdotnet.Contracts;
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


    public async Task AddToFlightHealthEventsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FlightHealthEvents
            .Where(flightHealthEvent =>
                request.ChildIds.Contains(flightHealthEvent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    flightHealthEvent =>
                        EF.Property<Guid?>(
                            flightHealthEvent,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFlightHealthEventsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FlightHealthEvents
            .Where(flightHealthEvent =>
                request.ChildIds.Contains(flightHealthEvent.Id) &&
                EF.Property<Guid?>(
                    flightHealthEvent,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    flightHealthEvent =>
                        EF.Property<Guid?>(
                            flightHealthEvent,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToSoftwareLoadsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SoftwareLoads
            .Where(softwareLoad =>
                request.ChildIds.Contains(softwareLoad.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    softwareLoad =>
                        EF.Property<Guid?>(
                            softwareLoad,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSoftwareLoadsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SoftwareLoads
            .Where(softwareLoad =>
                request.ChildIds.Contains(softwareLoad.Id) &&
                EF.Property<Guid?>(
                    softwareLoad,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    softwareLoad =>
                        EF.Property<Guid?>(
                            softwareLoad,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
