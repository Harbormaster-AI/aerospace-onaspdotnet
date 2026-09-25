
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftPackageRepository : IAircraftPackageRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftPackageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftPackages
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftPackage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftPackages
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Add(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Update(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftPackage aircraftPackage, CancellationToken cancellationToken)
    {
        _db.AircraftPackages.Remove(aircraftPackage);
        await _db.SaveChangesAsync(cancellationToken);
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
