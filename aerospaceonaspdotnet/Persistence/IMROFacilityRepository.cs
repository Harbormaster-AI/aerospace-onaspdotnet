using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IMROFacilityRepository
{
    Task<MROFacility?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MROFacility>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MROFacility mROFacility, CancellationToken cancellationToken);
    Task UpdateAsync(MROFacility mROFacility, CancellationToken cancellationToken);
    Task DeleteAsync(MROFacility mROFacility, CancellationToken cancellationToken);
}
