
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class BuildSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BuildscheduleId { get; set; } 
 public virtual string? ScheduleNumber { get; set; } 
public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();
 public virtual ScheduleStatus? Status { get; set; } 

    public static BuildSchedule FromRequest(BuildScheduleRequest request) {
        return new BuildSchedule {
            Id = request.Id,
            ScheduleNumber = request.ScheduleNumber,
            Status = request.Status,
        };
    }
}
