using aerospaceonaspdotnet.Domain;

namespace aerospaceonaspdotnet.Persistence;

public interface IAircraftProgramRepository
{
    Task<AircraftProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftProgram>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken);
    Task UpdateAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken);
    Task DeleteAsync(AircraftProgram aircraftProgram, CancellationToken cancellationToken);
}
