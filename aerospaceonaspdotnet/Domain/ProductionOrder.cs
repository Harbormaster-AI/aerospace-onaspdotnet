
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class ProductionOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductionorderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
public virtual AircraftVariant? Variant { get; set; } 
public virtual Plant? Plant { get; set; } 
public virtual AircraftOrder? AircraftOrder { get; set; } 
 public virtual ProductionOrderStatus? Status { get; set; } 

    public static ProductionOrder FromRequest(ProductionOrderRequest request) {
        return new ProductionOrder {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            Status = request.Status,
        };
    }
}
