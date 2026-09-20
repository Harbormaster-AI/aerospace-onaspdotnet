using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAerospaceManufacturerRepository
{
    Task<AerospaceManufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AerospaceManufacturer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);
    Task UpdateAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);
    Task DeleteAsync(AerospaceManufacturer aerospaceManufacturer, CancellationToken cancellationToken);
}
