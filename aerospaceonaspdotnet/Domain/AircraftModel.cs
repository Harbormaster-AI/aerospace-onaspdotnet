
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftmodelId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ModelDesignation { get; set; } 
public virtual AircraftFamily? Family { get; set; } 
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
public virtual ICollection<EngineType> EngineTypes { get; set; } = new List<EngineType>();
 public virtual AircraftType? AircraftType { get; set; } 

    public static AircraftModel FromRequest(AircraftModelRequest request) {
        return new AircraftModel {
            Id = request.Id,
            Name = request.Name,
            ModelDesignation = request.ModelDesignation,
            AircraftType = request.AircraftType,
        };
    }
}
