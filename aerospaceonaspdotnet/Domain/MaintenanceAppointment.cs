
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class MaintenanceAppointment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MaintenanceappointmentId { get; set; } 
 public virtual DateOnly? AppointmentDate { get; set; } 
public virtual Aircraft? Aircraft { get; set; } 
public virtual MROFacility? MroFacility { get; set; } 
public virtual MaintenanceWorkOrder? WorkOrder { get; set; } 
 public virtual AppointmentStatus? Status { get; set; } 

    public static MaintenanceAppointment FromRequest(MaintenanceAppointmentRequest request) {
        return new MaintenanceAppointment {
            Id = request.Id,
            AppointmentDate = request.AppointmentDate,
            Status = request.Status,
        };
    }
}
