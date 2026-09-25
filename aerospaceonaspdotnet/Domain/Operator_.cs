
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Operator_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Operator_Id { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? IcaoDesignator { get; set; } 
public virtual ICollection<AircraftOrder> AircraftOrders { get; set; } = new List<AircraftOrder>();
public virtual ICollection<Aircraft> OperatedAircraft { get; set; } = new List<Aircraft>();
public virtual SalesRegion? SalesRegion { get; set; } 
 public virtual OperatorType? OperatorType { get; set; } 

    public static Operator_ FromRequest(Operator_Request request) {
        return new Operator_ {
            Id = request.Id,
            Name = request.Name,
            IcaoDesignator = request.IcaoDesignator,
            OperatorType = request.OperatorType,
        };
    }
}
