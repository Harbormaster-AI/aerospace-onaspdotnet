using aerospaceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class MROFacilityRepository : IMROFacilityRepository
{
    private readonly ApplicationDbContext _db;

    public MROFacilityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MROFacility?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MROFacilitys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MROFacility>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MROFacilitys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Add(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Update(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MROFacility mROFacility, CancellationToken cancellationToken)
    {
        _db.MROFacilitys.Remove(mROFacility);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
