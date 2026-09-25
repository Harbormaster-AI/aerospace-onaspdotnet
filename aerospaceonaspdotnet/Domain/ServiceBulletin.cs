
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class ServiceBulletin
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ServicebulletinId { get; set; } 
 public virtual string? BulletinNumber { get; set; } 
public virtual ICollection<MaintenanceWorkOrder> WorkOrders { get; set; } = new List<MaintenanceWorkOrder>();
public virtual ICollection<AircraftVariant> Variants { get; set; } = new List<AircraftVariant>();
 public virtual ServiceBulletinCategory? Category { get; set; } 

    public static ServiceBulletin FromRequest(ServiceBulletinRequest request) {
        return new ServiceBulletin {
            Id = request.Id,
            BulletinNumber = request.BulletinNumber,
            Category = request.Category,
        };
    }
}
