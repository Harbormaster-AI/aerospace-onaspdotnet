
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class ProductionCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProductioncertificateId { get; set; } 
 public virtual string? CertificateNumber { get; set; } 
 public virtual string? Authority { get; set; } 
public virtual AerospaceManufacturer? Manufacturer { get; set; } 

    public static ProductionCertificate FromRequest(ProductionCertificateRequest request) {
        return new ProductionCertificate {
            Id = request.Id,
            CertificateNumber = request.CertificateNumber,
            Authority = request.Authority,
        };
    }
}
