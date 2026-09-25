using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Persistence;

public interface IPurchaseAgreementRepository
{
    Task<PurchaseAgreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PurchaseAgreement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken);
    Task UpdateAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken);
    Task DeleteAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken);


}
