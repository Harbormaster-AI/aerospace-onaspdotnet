using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IProductionOrderRepository
{
    Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProductionOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProductionOrder productionOrder, CancellationToken cancellationToken);
    Task UpdateAsync(ProductionOrder productionOrder, CancellationToken cancellationToken);
    Task DeleteAsync(ProductionOrder productionOrder, CancellationToken cancellationToken);


}
