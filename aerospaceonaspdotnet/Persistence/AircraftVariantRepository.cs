
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftVariantRepository : IAircraftVariantRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftVariantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftVariant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftVariants
            .Include(x => x.Model_)
            .Include(x => x.EngineType)
            .Include(x => x.AvionicsSuite)
            .Include(x => x.Apu)
            .Include(x => x.LandingGear)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftVariant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftVariants
            .AsNoTracking()
            .Include(x => x.Model_)
            .Include(x => x.EngineType)
            .Include(x => x.AvionicsSuite)
            .Include(x => x.Apu)
            .Include(x => x.LandingGear)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Add(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Update(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftVariant aircraftVariant, CancellationToken cancellationToken)
    {
        _db.AircraftVariants.Remove(aircraftVariant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCabinLayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CabinLayouts
            .Where(cabinLayout =>
                request.ChildIds.Contains(cabinLayout.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cabinLayout =>
                        EF.Property<Guid?>(
                            cabinLayout,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCabinLayoutsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CabinLayouts
            .Where(cabinLayout =>
                request.ChildIds.Contains(cabinLayout.Id) &&
                EF.Property<Guid?>(
                    cabinLayout,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    cabinLayout =>
                        EF.Property<Guid?>(
                            cabinLayout,
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
