using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface ILandingGearRepository
{
    Task<LandingGear?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LandingGear>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LandingGear landingGear, CancellationToken cancellationToken);
    Task UpdateAsync(LandingGear landingGear, CancellationToken cancellationToken);
    Task DeleteAsync(LandingGear landingGear, CancellationToken cancellationToken);

    Task AddToVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromVariantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
