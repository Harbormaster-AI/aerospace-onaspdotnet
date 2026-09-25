
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Component_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Component_Id { get; set; } 
 public virtual string? PartNumber { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Supplier? Supplier { get; set; } 
 public virtual ComponentCategory? ComponentCategory { get; set; } 
 public virtual SerializationMethod? SerializationMethod { get; set; } 

    public static Component_ FromRequest(Component_Request request) {
        return new Component_ {
            Id = request.Id,
            PartNumber = request.PartNumber,
            Name = request.Name,
            ComponentCategory = request.ComponentCategory,
            SerializationMethod = request.SerializationMethod,
        };
    }
}
