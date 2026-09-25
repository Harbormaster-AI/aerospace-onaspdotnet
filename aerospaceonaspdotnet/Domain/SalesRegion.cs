
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class SalesRegion
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SalesregionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? RegionCode { get; set; } 
public virtual ICollection<Operator_> Operators { get; set; } = new List<Operator_>();
public virtual ICollection<SalesCampaign> SalesCampaigns { get; set; } = new List<SalesCampaign>();

    public static SalesRegion FromRequest(SalesRegionRequest request) {
        return new SalesRegion {
            Id = request.Id,
            Name = request.Name,
            RegionCode = request.RegionCode,
        };
    }
}
