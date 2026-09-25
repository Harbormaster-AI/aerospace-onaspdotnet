
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class ConnectedAircraft
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConnectedaircraftId { get; set; } 
 public virtual string? CommunicationsProvider { get; set; } 
public virtual Aircraft? Aircraft { get; set; } 
public virtual ICollection<FlightHealthEvent> FlightHealthEvents { get; set; } = new List<FlightHealthEvent>();
public virtual ICollection<SoftwareLoad> SoftwareLoads { get; set; } = new List<SoftwareLoad>();
 public virtual ConnectivityStatus? ConnectivityStatus { get; set; } 

    public static ConnectedAircraft FromRequest(ConnectedAircraftRequest request) {
        return new ConnectedAircraft {
            Id = request.Id,
            CommunicationsProvider = request.CommunicationsProvider,
            ConnectivityStatus = request.ConnectivityStatus,
        };
    }
}
