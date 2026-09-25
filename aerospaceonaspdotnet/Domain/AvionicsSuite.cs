
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AvionicsSuite
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AvionicssuiteId { get; set; } 
 public virtual string? SuiteName { get; set; } 
 public virtual string? SoftwareBaseline { get; set; } 
public virtual Supplier? Supplier { get; set; } 
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
public virtual ICollection<SoftwareLoad> SoftwareLoads { get; set; } = new List<SoftwareLoad>();

    public static AvionicsSuite FromRequest(AvionicsSuiteRequest request) {
        return new AvionicsSuite {
            Id = request.Id,
            SuiteName = request.SuiteName,
            SoftwareBaseline = request.SoftwareBaseline,
        };
    }
}
