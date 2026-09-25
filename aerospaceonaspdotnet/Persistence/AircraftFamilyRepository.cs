
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class AircraftFamilyRepository : IAircraftFamilyRepository
{
    private readonly ApplicationDbContext _db;

    public AircraftFamilyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AircraftFamily?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AircraftFamilys
            .Include(x => x.Program)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AircraftFamily>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AircraftFamilys
            .AsNoTracking()
            .Include(x => x.Program)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Add(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Update(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AircraftFamily aircraftFamily, CancellationToken cancellationToken)
    {
        _db.AircraftFamilys.Remove(aircraftFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAircraftModelsAsync(
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

    public async Task RemoveFromAircraftModelsAsync(
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
