
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class TypeCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TypecertificateId { get; set; } 
 public virtual string? CertificateNumber { get; set; } 
 public virtual string? Authority { get; set; } 
public virtual AircraftProgram? Program { get; set; } 

    public static TypeCertificate FromRequest(TypeCertificateRequest request) {
        return new TypeCertificate {
            Id = request.Id,
            CertificateNumber = request.CertificateNumber,
            Authority = request.Authority,
        };
    }
}
