
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftModelRepository : IAircraftModelRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftModelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftModels
            .Include(x => x.Family)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftModels
            .AsNoTracking()
            .Include(x => x.Family)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Add(aircraftModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Update(aircraftModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftModel aircraftModel, CancellationToken cancellationToken)
    {
        _db.AircraftModels.Remove(aircraftModel);
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


    public async Task AddToEngineTypesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EngineTypes
            .Where(engineType =>
                request.ChildIds.Contains(engineType.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    engineType =>
                        EF.Property<Guid?>(
                            engineType,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEngineTypesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EngineTypes
            .Where(engineType =>
                request.ChildIds.Contains(engineType.Id) &&
                EF.Property<Guid?>(
                    engineType,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    engineType =>
                        EF.Property<Guid?>(
                            engineType,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
