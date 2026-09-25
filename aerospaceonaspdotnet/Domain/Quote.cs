
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class Quote
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? QuoteId { get; set; } 
 public virtual string? QuoteNumber { get; set; } 
 public virtual Money? TotalAmount { get; set; } 
public virtual AircraftOrder? AircraftOrder { get; set; } 

    public static Quote FromRequest(QuoteRequest request) {
        return new Quote {
            Id = request.Id,
            QuoteNumber = request.QuoteNumber,
            TotalAmount = request.TotalAmount,
        };
    }
}
