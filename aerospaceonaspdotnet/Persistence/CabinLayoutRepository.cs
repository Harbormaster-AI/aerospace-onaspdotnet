
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class CabinLayoutRepository : ICabinLayoutRepository
{
    private readonly ApplicationDbContext _db;

    public CabinLayoutRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CabinLayout?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CabinLayouts
            .Include(x => x.Variant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CabinLayout>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CabinLayouts
            .AsNoTracking()
            .Include(x => x.Variant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CabinLayout cabinLayout, CancellationToken cancellationToken)
    {
        _db.CabinLayouts.Add(cabinLayout);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CabinLayout cabinLayout, CancellationToken cancellationToken)
    {
        _db.CabinLayouts.Update(cabinLayout);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CabinLayout cabinLayout, CancellationToken cancellationToken)
    {
        _db.CabinLayouts.Remove(cabinLayout);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAircraftAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Aircrafts
            .Where(aircraft =>
                request.ChildIds.Contains(aircraft.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraft =>
                        EF.Property<Guid?>(
                            aircraft,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAircraftAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Aircrafts
            .Where(aircraft =>
                request.ChildIds.Contains(aircraft.Id) &&
                EF.Property<Guid?>(
                    aircraft,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraft =>
                        EF.Property<Guid?>(
                            aircraft,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToOptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftOptions
            .Where(aircraftOption =>
                request.ChildIds.Contains(aircraftOption.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftOption =>
                        EF.Property<Guid?>(
                            aircraftOption,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftOptions
            .Where(aircraftOption =>
                request.ChildIds.Contains(aircraftOption.Id) &&
                EF.Property<Guid?>(
                    aircraftOption,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftOption =>
                        EF.Property<Guid?>(
                            aircraftOption,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
