
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Aircraft
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftId { get; set; } 
 public virtual MSN? Msn { get; set; } 
 public virtual DateOnly? DeliveryDate { get; set; } 
public virtual AircraftVariant? Variant { get; set; } 
public virtual Operator_? Operator_ { get; set; } 
public virtual Registration? Registration { get; set; } 
public virtual Warranty? Warranty { get; set; } 
public virtual ICollection<MaintenanceWorkOrder> MaintenanceRecords { get; set; } = new List<MaintenanceWorkOrder>();
public virtual ConnectedAircraft? ConnectedAircraft { get; set; } 
public virtual CabinLayout? CabinLayout { get; set; } 

    public static Aircraft FromRequest(AircraftRequest request) {
        return new Aircraft {
            Id = request.Id,
            Msn = request.Msn,
            DeliveryDate = request.DeliveryDate,
        };
    }
}
