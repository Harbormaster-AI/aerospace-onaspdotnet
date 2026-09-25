
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class SoftwareLoad
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SoftwareloadId { get; set; } 
 public virtual string? Version { get; set; } 
public virtual ConnectedAircraft? ConnectedAircraft { get; set; } 
public virtual AvionicsSuite? AvionicsSuite { get; set; } 
 public virtual SoftwareLoadType? LoadType { get; set; } 

    public static SoftwareLoad FromRequest(SoftwareLoadRequest request) {
        return new SoftwareLoad {
            Id = request.Id,
            Version = request.Version,
            LoadType = request.LoadType,
        };
    }
}
