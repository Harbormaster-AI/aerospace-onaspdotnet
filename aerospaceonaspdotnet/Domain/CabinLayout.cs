
using aerospaceonaspdotnet.Contracts;

namespace aerospaceonaspdotnet.Domain;

public class CabinLayout
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CabinlayoutId { get; set; } 
 public virtual string? LayoutCode { get; set; } 
 public virtual int? TotalSeats { get; set; } 
 public virtual string? ClassLayout { get; set; } 
public virtual AircraftVariant? Variant { get; set; } 
public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
public virtual ICollection<AircraftOption> Options { get; set; } = new List<AircraftOption>();

    public static CabinLayout FromRequest(CabinLayoutRequest request) {
        return new CabinLayout {
            Id = request.Id,
            LayoutCode = request.LayoutCode,
            TotalSeats = request.TotalSeats,
            ClassLayout = request.ClassLayout,
        };
    }
}
