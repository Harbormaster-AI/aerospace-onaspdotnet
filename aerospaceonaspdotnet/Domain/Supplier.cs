
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Supplier
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SupplierId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<AerospaceManufacturer> Manufacturers { get; set; } = new List<AerospaceManufacturer>();
public virtual ICollection<Component_> Components { get; set; } = new List<Component_>();
public virtual ICollection<EngineType> EngineTypes { get; set; } = new List<EngineType>();
public virtual ICollection<AvionicsSuite> AvionicsSuites { get; set; } = new List<AvionicsSuite>();
public virtual ICollection<APU> Apus { get; set; } = new List<APU>();
public virtual ICollection<LandingGear> LandingGears { get; set; } = new List<LandingGear>();
 public virtual SupplierType? SupplierType { get; set; } 
 public virtual SupplierApprovalStatus? ApprovalStatus { get; set; } 

    public static Supplier FromRequest(SupplierRequest request) {
        return new Supplier {
            Id = request.Id,
            Name = request.Name,
            SupplierType = request.SupplierType,
            ApprovalStatus = request.ApprovalStatus,
        };
    }
}
