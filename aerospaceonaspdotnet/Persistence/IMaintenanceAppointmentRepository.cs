using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IMaintenanceAppointmentRepository
{
    Task<MaintenanceAppointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceAppointment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken);
    Task UpdateAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenanceAppointment maintenanceAppointment, CancellationToken cancellationToken);


}
