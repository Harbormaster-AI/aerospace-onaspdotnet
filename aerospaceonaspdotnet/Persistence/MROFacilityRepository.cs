
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class MROFacilityRepository : IMROFacilityRepository
{
    private readonly ApplicationDbContext _db;

    public MROFacilityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MROFacility?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MROFacilitys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MROFacility>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MROFacilitys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Add(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Update(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Remove(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceAppointments
            .Where(maintenanceAppointment =>
                request.ChildIds.Contains(maintenanceAppointment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceAppointment =>
                        EF.Property<Guid?>(
                            maintenanceAppointment,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAppointmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceAppointments
            .Where(maintenanceAppointment =>
                request.ChildIds.Contains(maintenanceAppointment.Id) &&
                EF.Property<Guid?>(
                    maintenanceAppointment,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceAppointment =>
                        EF.Property<Guid?>(
                            maintenanceAppointment,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }


    public async Task AddToWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceWorkOrders
            .Where(maintenanceWorkOrder =>
                request.ChildIds.Contains(maintenanceWorkOrder.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceWorkOrder =>
                        EF.Property<Guid?>(
                            maintenanceWorkOrder,
                            "SalesCampaign_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkOrdersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceWorkOrders
            .Where(maintenanceWorkOrder =>
                request.ChildIds.Contains(maintenanceWorkOrder.Id) &&
                EF.Property<Guid?>(
                    maintenanceWorkOrder,
                    "SalesCampaign_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceWorkOrder =>
                        EF.Property<Guid?>(
                            maintenanceWorkOrder,
                            "SalesCampaign_Id"),
                    (Guid?)null));
    }

}
