
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class WorkCenter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkcenterId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Capability { get; set; } 
public virtual ProductionLine? ProductionLine { get; set; } 

    public static WorkCenter FromRequest(WorkCenterRequest request) {
        return new WorkCenter {
            Id = request.Id,
            Name = request.Name,
            Capability = request.Capability,
        };
    }
}
