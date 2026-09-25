
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class MaintenanceWorkOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MaintenanceworkorderId { get; set; } 
 public virtual string? WorkOrderNumber { get; set; } 
public virtual Aircraft? Aircraft { get; set; } 
public virtual AirworthinessDirective? AirworthinessDirective { get; set; } 
public virtual ServiceBulletin? ServiceBulletin { get; set; } 
 public virtual WorkOrderStatus? Status { get; set; } 

    public static MaintenanceWorkOrder FromRequest(MaintenanceWorkOrderRequest request) {
        return new MaintenanceWorkOrder {
            Id = request.Id,
            WorkOrderNumber = request.WorkOrderNumber,
            Status = request.Status,
        };
    }
}
