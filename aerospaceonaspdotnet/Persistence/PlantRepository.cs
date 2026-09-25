
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class PlantRepository : IPlantRepository
{
    private readonly ApplicationDbContext _db;

    public PlantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Plants
            .Include(x => x.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Plant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Plants
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Add(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Update(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Plant plant, CancellationToken cancellationToken)
    {
        _db.Plants.Remove(plant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProductionLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionLines
            .Where(productionLine =>
                request.ChildIds.Contains(productionLine.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionLine =>
                        EF.Property<Guid?>(
                            productionLine,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductionLinesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ProductionLines
            .Where(productionLine =>
                request.ChildIds.Contains(productionLine.Id) &&
                EF.Property<Guid?>(
                    productionLine,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    productionLine =>
                        EF.Property<Guid?>(
                            productionLine,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToWarehousesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Warehouses
            .Where(warehouse =>
                request.ChildIds.Contains(warehouse.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    warehouse =>
                        EF.Property<Guid?>(
                            warehouse,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWarehousesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Warehouses
            .Where(warehouse =>
                request.ChildIds.Contains(warehouse.Id) &&
                EF.Property<Guid?>(
                    warehouse,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    warehouse =>
                        EF.Property<Guid?>(
                            warehouse,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
