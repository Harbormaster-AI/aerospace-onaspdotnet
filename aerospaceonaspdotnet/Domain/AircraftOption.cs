
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftOption
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftoptionId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
public virtual ICollection<AircraftPackage> Packages { get; set; } = new List<AircraftPackage>();
 public virtual OptionCategory? OptionCategory { get; set; } 

    public static AircraftOption FromRequest(AircraftOptionRequest request) {
        return new AircraftOption {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
            OptionCategory = request.OptionCategory,
        };
    }
}
