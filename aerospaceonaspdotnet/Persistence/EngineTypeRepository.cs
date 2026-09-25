
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class EngineTypeRepository : IEngineTypeRepository
{
    private readonly ApplicationDbContext _db;

    public EngineTypeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EngineType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.EngineTypes
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EngineType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.EngineTypes
            .AsNoTracking()
            .Include(x => x.Supplier)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EngineType engineType, CancellationToken cancellationToken)
    {
        _db.EngineTypes.Add(engineType);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EngineType engineType, CancellationToken cancellationToken)
    {
        _db.EngineTypes.Update(engineType);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EngineType engineType, CancellationToken cancellationToken)
    {
        _db.EngineTypes.Remove(engineType);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCompatibleModelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftModels
            .Where(aircraftModel =>
                request.ChildIds.Contains(aircraftModel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftModel =>
                        EF.Property<Guid?>(
                            aircraftModel,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCompatibleModelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftModels
            .Where(aircraftModel =>
                request.ChildIds.Contains(aircraftModel.Id) &&
                EF.Property<Guid?>(
                    aircraftModel,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftModel =>
                        EF.Property<Guid?>(
                            aircraftModel,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
