
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class Operator_Repository : IOperator_Repository
{
    private readonly ApplicationDbContext _db;

    public Operator_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Operator_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Operator_s
            .Include(x => x.SalesRegion)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Operator_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Operator_s
            .AsNoTracking()
            .Include(x => x.SalesRegion)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Operator_ operator_, CancellationToken cancellationToken)
    {
        _db.Operator_s.Add(operator_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Operator_ operator_, CancellationToken cancellationToken)
    {
        _db.Operator_s.Update(operator_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Operator_ operator_, CancellationToken cancellationToken)
    {
        _db.Operator_s.Remove(operator_);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAircraftOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftOrders
            .Where(aircraftOrder =>
                request.ChildIds.Contains(aircraftOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftOrder =>
                        EF.Property<Guid?>(
                            aircraftOrder,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAircraftOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AircraftOrders
            .Where(aircraftOrder =>
                request.ChildIds.Contains(aircraftOrder.Id) &&
                EF.Property<Guid?>(
                    aircraftOrder,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraftOrder =>
                        EF.Property<Guid?>(
                            aircraftOrder,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToOperatedAircraftAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Aircrafts
            .Where(aircraft =>
                request.ChildIds.Contains(aircraft.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraft =>
                        EF.Property<Guid?>(
                            aircraft,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOperatedAircraftAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Aircrafts
            .Where(aircraft =>
                request.ChildIds.Contains(aircraft.Id) &&
                EF.Property<Guid?>(
                    aircraft,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    aircraft =>
                        EF.Property<Guid?>(
                            aircraft,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
