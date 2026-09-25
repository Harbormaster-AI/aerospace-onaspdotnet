
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class PurchaseAgreement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PurchaseagreementId { get; set; } 
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
public virtual AircraftOrder? AircraftOrder { get; set; } 

    public static PurchaseAgreement FromRequest(PurchaseAgreementRequest request) {
        return new PurchaseAgreement {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectiveDate = request.EffectiveDate,
        };
    }
}
