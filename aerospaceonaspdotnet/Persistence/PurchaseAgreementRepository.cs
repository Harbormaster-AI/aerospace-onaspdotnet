
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class PurchaseAgreementRepository : IPurchaseAgreementRepository
{
    private readonly ApplicationDbContext _db;

    public PurchaseAgreementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PurchaseAgreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PurchaseAgreements
            .Include(x => x.AircraftOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PurchaseAgreement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PurchaseAgreements
            .AsNoTracking()
            .Include(x => x.AircraftOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken)
    {
        _db.PurchaseAgreements.Add(purchaseAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken)
    {
        _db.PurchaseAgreements.Update(purchaseAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PurchaseAgreement purchaseAgreement, CancellationToken cancellationToken)
    {
        _db.PurchaseAgreements.Remove(purchaseAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
