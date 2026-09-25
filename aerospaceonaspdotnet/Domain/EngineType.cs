
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class EngineType
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EnginetypeId { get; set; } 
 public virtual string? EngineModelCode { get; set; } 
 public virtual decimal? MaxThrustKn { get; set; } 
public virtual Supplier? Supplier { get; set; } 
public virtual ICollection<AircraftModel> CompatibleModels { get; set; } = new List<AircraftModel>();
 public virtual EngineCategory? Category { get; set; } 

    public static EngineType FromRequest(EngineTypeRequest request) {
        return new EngineType {
            Id = request.Id,
            EngineModelCode = request.EngineModelCode,
            MaxThrustKn = request.MaxThrustKn,
            Category = request.Category,
        };
    }
}
