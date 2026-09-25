
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class LandingGearRepository : ILandingGearRepository
{
    private readonly ApplicationDbContext _db;

    public LandingGearRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LandingGear?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LandingGears
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LandingGear>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LandingGears
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Add(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Update(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LandingGear landingGear, CancellationToken cancellationToken)
    {
        _db.LandingGears.Remove(landingGear);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToVariantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftVariants
            .Where(aircraftVariant =>
                request.ChildIds.Contains(aircraftVariant.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftVariant =>
                        EF.Property<Guid?>(
                            aircraftVariant,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromVariantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftVariants
            .Where(aircraftVariant =>
                request.ChildIds.Contains(aircraftVariant.Id) &&
                EF.Property<Guid?>(
                    aircraftVariant,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftVariant =>
                        EF.Property<Guid?>(
                            aircraftVariant,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
