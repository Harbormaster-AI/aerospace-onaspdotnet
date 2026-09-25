
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class ProductionCertificateRepository : IProductionCertificateRepository
{
    private readonly ApplicationDbContext _db;

    public ProductionCertificateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductionCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProductionCertificates
            .Include(x => x.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionCertificate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProductionCertificates
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken)
    {
        _db.ProductionCertificates.Add(productionCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken)
    {
        _db.ProductionCertificates.Update(productionCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductionCertificate productionCertificate, CancellationToken cancellationToken)
    {
        _db.ProductionCertificates.Remove(productionCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
