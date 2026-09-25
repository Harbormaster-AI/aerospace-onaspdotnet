
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftOptionRepository : IAircraftOptionRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftOptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftOptions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftOption>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftOptions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Add(aircraftOption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Update(aircraftOption);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftOption aircraftOption, CancellationToken cancellationToken)
    {
        _db.AircraftOptions.Remove(aircraftOption);
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


    public async Task AddToPackagesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftPackages
            .Where(aircraftPackage =>
                request.ChildIds.Contains(aircraftPackage.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftPackage =>
                        EF.Property<Guid?>(
                            aircraftPackage,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPackagesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftPackages
            .Where(aircraftPackage =>
                request.ChildIds.Contains(aircraftPackage.Id) &&
                EF.Property<Guid?>(
                    aircraftPackage,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftPackage =>
                        EF.Property<Guid?>(
                            aircraftPackage,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
