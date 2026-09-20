using aerospaceonaspdotnet.Domain;
using aerospaceonaspdotnet.Persistence;
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Service;

public interface IAircraftProgramService {

    Task Create(AircraftProgram model , CancellationToken cancellationToken);
    Task<bool> Update(AircraftProgram model, CancellationToken cancellationToken);
    Task<AircraftProgram?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AircraftProgram>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTypeCertificate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTypeCertificate(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAircraftFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAircraftFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToKeySuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromKeySuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AircraftProgramService : IAircraftProgramService
{
    private readonly IAircraftProgramRepository _repository;
    private readonly ILogger<AircraftProgramService> _logger;

    public AircraftProgramService(
        IAircraftProgramRepository repository, ILogger<AircraftProgramService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AircraftProgram model, CancellationToken cancellationToken)
    {

 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(AircraftProgram model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ProgramCode = model.ProgramCode;
            existing.EntryIntoServiceYear = model.EntryIntoServiceYear;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AircraftProgram?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AircraftProgram>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManufacturer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTypeCertificate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTypeCertificate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAircraftFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAircraftFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToKeySuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromKeySuppliers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
