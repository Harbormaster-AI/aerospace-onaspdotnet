using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IBuildScheduleRepository
{
    Task<BuildSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BuildSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(BuildSchedule buildSchedule, CancellationToken cancellationToken);
}
