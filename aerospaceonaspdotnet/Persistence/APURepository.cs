
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class APURepository : IAPURepository
{
    private readonly ApplicationDbContext _db;

    public APURepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<APU?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.APUs
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<APU>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.APUs
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(APU aPU, CancellationToken cancellationToken)
    {
        _db.APUs.Add(aPU);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(APU aPU, CancellationToken cancellationToken)
    {
        _db.APUs.Update(aPU);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(APU aPU, CancellationToken cancellationToken)
    {
        _db.APUs.Remove(aPU);
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
