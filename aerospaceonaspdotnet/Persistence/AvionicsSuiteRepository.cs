
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AvionicsSuiteRepository : IAvionicsSuiteRepository
{
    private readonly ApplicationDbContext _db;

    public AvionicsSuiteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AvionicsSuite?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AvionicsSuites
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AvionicsSuite>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AvionicsSuites
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Add(avionicsSuite);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Update(avionicsSuite);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AvionicsSuite avionicsSuite, CancellationToken cancellationToken)
    {
        _db.AvionicsSuites.Remove(avionicsSuite);
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
