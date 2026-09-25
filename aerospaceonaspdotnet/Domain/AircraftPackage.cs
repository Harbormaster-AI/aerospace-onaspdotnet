
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftPackage
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftpackageId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<AircraftOption> Options { get; set; } = new List<AircraftOption>();
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
 public virtual PackageType? PackageType { get; set; } 

    public static AircraftPackage FromRequest(AircraftPackageRequest request) {
        return new AircraftPackage {
            Id = request.Id,
            Name = request.Name,
            PackageType = request.PackageType,
        };
    }
}
