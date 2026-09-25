
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class InventoryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InventoryitemId { get; set; } 
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
 public virtual string? LotNumber { get; set; } 
public virtual Component_? Component { get; set; } 
public virtual Warehouse? Warehouse { get; set; } 

    public static InventoryItem FromRequest(InventoryItemRequest request) {
        return new InventoryItem {
            Id = request.Id,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
            LotNumber = request.LotNumber,
        };
    }
}
