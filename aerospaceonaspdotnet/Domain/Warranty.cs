
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Warranty
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WarrantyId { get; set; } 
 public virtual int? CoverageMonths { get; set; } 
public virtual Aircraft? Aircraft { get; set; } 
 public virtual WarrantyType? WarrantyType { get; set; } 

    public static Warranty FromRequest(WarrantyRequest request) {
        return new Warranty {
            Id = request.Id,
            CoverageMonths = request.CoverageMonths,
            WarrantyType = request.WarrantyType,
        };
    }
}
