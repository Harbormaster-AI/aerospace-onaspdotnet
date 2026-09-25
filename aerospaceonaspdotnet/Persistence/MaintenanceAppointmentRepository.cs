
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class MaintenanceAppointmentRepository : IMaintenanceAppointmentRepository
{
    private readonly ApplicationDbContext _db;

    public MaintenanceAppointmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MaintenanceAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MaintenanceAppointments
            .Include(x => x.Aircraft)
            .Include(x => x.MroFacility)
            .Include(x => x.WorkOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenanceAppointment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MaintenanceAppointments
            .AsNoTracking()
            .Include(x => x.Aircraft)
            .Include(x => x.MroFacility)
            .Include(x => x.WorkOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken)
    {
        _db.MaintenanceAppointments.Add(maintenanceAppointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken)
    {
        _db.MaintenanceAppointments.Update(maintenanceAppointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken)
    {
        _db.MaintenanceAppointments.Remove(maintenanceAppointment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
