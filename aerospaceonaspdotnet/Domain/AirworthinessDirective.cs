
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AirworthinessDirective
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AirworthinessdirectiveId { get; set; } 
 public virtual string? DirectiveNumber { get; set; } 
 public virtual string? Title { get; set; } 
public virtual ICollection<MaintenanceWorkOrder> WorkOrders { get; set; } = new List<MaintenanceWorkOrder>();

    public static AirworthinessDirective FromRequest(AirworthinessDirectiveRequest request) {
        return new AirworthinessDirective {
            Id = request.Id,
            DirectiveNumber = request.DirectiveNumber,
            Title = request.Title,
        };
    }
}
