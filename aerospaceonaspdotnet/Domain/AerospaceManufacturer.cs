
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class AerospaceManufacturer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AerospacemanufacturerId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<AircraftProgram> Programs { get; set; } = new List<AircraftProgram>();
public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
public virtual ICollection<ProductionCertificate> ProductionCertificates { get; set; } = new List<ProductionCertificate>();

    public static AerospaceManufacturer FromRequest(AerospaceManufacturerRequest request) {
        return new AerospaceManufacturer {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
    }
}
