
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class SalesCampaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SalescampaignId { get; set; } 
 public virtual string? CampaignCode { get; set; } 
public virtual SalesRegion? Region { get; set; } 
public virtual Operator_? Operator_ { get; set; } 
public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
 public virtual SalesCampaignStatus? Status { get; set; } 

    public static SalesCampaign FromRequest(SalesCampaignRequest request) {
        return new SalesCampaign {
            Id = request.Id,
            CampaignCode = request.CampaignCode,
            Status = request.Status,
        };
    }
}
