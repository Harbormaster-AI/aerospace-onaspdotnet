using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IMaintenanceWorkOrderRepository
{
    Task<MaintenanceWorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceWorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken);
    Task UpdateAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenanceWorkOrder maintenanceWorkOrder, CancellationToken cancellationToken);


}
