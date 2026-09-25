
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftFamily
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftfamilyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? FamilyCode { get; set; } 
public virtual AircraftProgram? Program { get; set; } 
public virtual ICollection<AircraftModel> AircraftModels { get; set; } = new List<AircraftModel>();

    public static AircraftFamily FromRequest(AircraftFamilyRequest request) {
        return new AircraftFamily {
            Id = request.Id,
            Name = request.Name,
            FamilyCode = request.FamilyCode,
        };
    }
}
