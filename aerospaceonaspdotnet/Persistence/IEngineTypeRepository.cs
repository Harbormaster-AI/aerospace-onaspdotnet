using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IEngineTypeRepository
{
    Task<EngineType?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EngineType>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(EngineType engineType, CancellationToken cancellationToken);
    Task UpdateAsync(EngineType engineType, CancellationToken cancellationToken);
    Task DeleteAsync(EngineType engineType, CancellationToken cancellationToken);

    Task AddToCompatibleModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCompatibleModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
