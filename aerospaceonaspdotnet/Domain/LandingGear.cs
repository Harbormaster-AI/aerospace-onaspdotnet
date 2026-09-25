
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class LandingGear
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LandinggearId { get; set; } 
 public virtual string? SupplierPartNumber { get; set; } 
public virtual Supplier? Supplier { get; set; } 
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
 public virtual LandingGearType? GearType { get; set; } 

    public static LandingGear FromRequest(LandingGearRequest request) {
        return new LandingGear {
            Id = request.Id,
            SupplierPartNumber = request.SupplierPartNumber,
            GearType = request.GearType,
        };
    }
}
