using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IMROFacilityRepository
{
    Task<MROFacility?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MROFacility>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MROFacility mROFacility, CancellationToken cancellationToken);
    Task UpdateAsync(MROFacility mROFacility, CancellationToken cancellationToken);
    Task DeleteAsync(MROFacility mROFacility, CancellationToken cancellationToken);

    Task AddToAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAppointmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkOrdersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
