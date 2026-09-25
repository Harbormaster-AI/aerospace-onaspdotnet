
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class APU
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ApuId { get; set; } 
 public virtual string? Model_ { get; set; } 
public virtual Supplier? Supplier { get; set; } 
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();

    public static APU FromRequest(APURequest request) {
        return new APU {
            Id = request.Id,
            Model_ = request.Model_,
        };
    }
}
