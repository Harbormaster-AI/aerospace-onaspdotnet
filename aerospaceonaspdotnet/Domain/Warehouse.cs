
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Warehouse
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WarehouseId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    public static Warehouse FromRequest(WarehouseRequest request) {
        return new Warehouse {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
