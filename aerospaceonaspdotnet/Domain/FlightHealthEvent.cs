
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class FlightHealthEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FlighthealtheventId { get; set; } 
 public virtual string? EventCode { get; set; } 
public virtual ConnectedAircraft? ConnectedAircraft { get; set; } 
 public virtual EventSeverity? Severity { get; set; } 

    public static FlightHealthEvent FromRequest(FlightHealthEventRequest request) {
        return new FlightHealthEvent {
            Id = request.Id,
            EventCode = request.EventCode,
            Severity = request.Severity,
        };
    }
}
