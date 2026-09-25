
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AircraftOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AircraftorderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
public virtual Operator_? Operator_ { get; set; } 
public virtual AircraftVariant? Variant { get; set; } 
public virtual Quote? Quote { get; set; } 
public virtual PurchaseAgreement? PurchaseAgreement { get; set; } 
 public virtual AircraftOrderStatus? Status { get; set; } 

    public static AircraftOrder FromRequest(AircraftOrderRequest request) {
        return new AircraftOrder {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
        };
    }
}
