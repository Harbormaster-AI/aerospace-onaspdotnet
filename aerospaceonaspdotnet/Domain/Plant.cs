
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Plant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PlantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? PlantCode { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual AerospaceManufacturer? Manufacturer { get; set; } 
public virtual ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

    public static Plant FromRequest(PlantRequest request) {
        return new Plant {
            Id = request.Id,
            Name = request.Name,
            PlantCode = request.PlantCode,
            Address = request.Address,
        };
    }
}
