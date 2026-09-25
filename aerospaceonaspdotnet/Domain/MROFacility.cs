
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class MROFacility
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MrofacilityId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ApprovalScope { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual ICollection<MaintenanceAppointment> Appointments { get; set; } = new List<MaintenanceAppointment>();
public virtual ICollection<MaintenanceWorkOrder> WorkOrders { get; set; } = new List<MaintenanceWorkOrder>();

    public static MROFacility FromRequest(MROFacilityRequest request) {
        return new MROFacility {
            Id = request.Id,
            Name = request.Name,
            ApprovalScope = request.ApprovalScope,
            Address = request.Address,
        };
    }
}
