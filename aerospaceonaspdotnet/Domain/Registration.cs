
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Registration
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RegistrationId { get; set; } 
 public virtual TailNumber? TailNumber { get; set; } 
 public virtual string? RegistryCountry { get; set; } 
public virtual Aircraft? Aircraft { get; set; } 

    public static Registration FromRequest(RegistrationRequest request) {
        return new Registration {
            Id = request.Id,
            TailNumber = request.TailNumber,
            RegistryCountry = request.RegistryCountry,
        };
    }
}
