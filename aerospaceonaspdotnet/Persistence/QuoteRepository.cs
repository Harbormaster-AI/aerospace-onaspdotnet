
using aerospaceonaspdotnet.Contracts;
using aerospaceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace aerospaceonaspdotnet.Persistence;

public class QuoteRepository : IQuoteRepository
{
    private readonly ApplicationDbContext _db;

    public QuoteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Quotes
            .Include(x => x.AircraftOrder)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Quote>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Quotes
            .AsNoTracking()
            .Include(x => x.AircraftOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Add(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Update(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Remove(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
