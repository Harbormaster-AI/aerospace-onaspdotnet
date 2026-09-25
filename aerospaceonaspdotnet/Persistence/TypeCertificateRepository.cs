
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class TypeCertificateRepository : ITypeCertificateRepository
{
    private readonly ApplicationDbContext _db;

    public TypeCertificateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TypeCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TypeCertificates
            .Include(x => x.Program)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TypeCertificate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TypeCertificates
            .AsNoTracking()
            .Include(x => x.Program)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken)
    {
        _db.TypeCertificates.Add(typeCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken)
    {
        _db.TypeCertificates.Update(typeCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TypeCertificate typeCertificate, CancellationToken cancellationToken)
    {
        _db.TypeCertificates.Remove(typeCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
